using Plugin.Sync.Commerce.Messaging.Models;
using Plugin.Sync.Commerce.Messaging.Pipelines.Arguments;
using Plugin.Sync.Commerce.Messaging.Policies;
using Serilog;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Plugin.Sync.Commerce.Messaging.Pipelines.Blocks
{
    /// <summary>
    /// Sends SMS message with Twilio, more details here: https://www.twilio.com/
    /// </summary>
    public class SendSmsBlock : PipelineBlock<SendTwilioSmsArgument, SendMessageResult, CommercePipelineExecutionContext>
    {
        public override async Task<SendMessageResult> Run(SendTwilioSmsArgument arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg, "SendTwilioSmsArgument is required").IsNotNull();
            Condition.Requires(arg.SmsMessage, "SendTwilioSmsArgument.SmsMessage is required").IsNotNull();
            Condition.Requires(arg.SmsMessage.FromPhoneNumber, "SendTwilioSmsArgument.SmsMessage.FromPhoneNumber is required").IsNotNullOrEmpty();
            Condition.Requires(arg.SmsMessage.ToPhoneNumber, "SendTwilioSmsArgument.SmsMessage.ToPhoneNumber is required").IsNotNullOrEmpty();
            Condition.Requires(arg.SmsMessage.MessageBody, "SendTwilioSmsArgument.SmsMessage.MessageBody is required").IsNotNullOrEmpty();

            try
            {
                var smsTwilioConfigurationPolicy = context.GetPolicy<SmsTwilioConfigurationPolicy>();

                TwilioClient.Init(smsTwilioConfigurationPolicy.AccountSid, smsTwilioConfigurationPolicy.AuthToken);

                var message = await MessageResource.CreateAsync(
                    body: arg.SmsMessage.MessageBody,
                    from: new Twilio.Types.PhoneNumber(arg.SmsMessage.FromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(arg.SmsMessage.ToPhoneNumber)
                );

                //Log.Information($"SMS sent to phone number: {toPhoneNumber}, Message: {messageBody}, Message SID: {smsMessage.Sid}");
                return new SendMessageResult
                {
                    ErrorMessage = message.ErrorMessage,
                    ErrorCode = message.ErrorCode,
                    Success = message.ErrorCode == null || message.ErrorCode == 0
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error sending SMS message. Error: {ex.Message}");
                throw ex;
            }
        }
    }
}
