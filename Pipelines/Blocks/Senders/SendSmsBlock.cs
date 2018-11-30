using Serilog;
using Sitecore.Commerce.Core;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using XCentium.Sitecore.Commerce.Messages.Policies;

namespace XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks
{
    /// <summary>
    /// Sends SMS message with Twilio, more details here: https://www.twilio.com/
    /// </summary>
    public class SendSmsBlock : PipelineMessageBlockBase
    {
#pragma warning disable 1998
        public override async Task<CommerceEntity> Run(CommerceEntity entity, CommercePipelineExecutionContext context)
        {
            try
            {
                var message = GetMessage(context);

                if (!message.ContainsProperty(Constants.Keys.Body) || string.IsNullOrEmpty(message.GetPropertyValue(Constants.Keys.Body) as string))
                {
                    throw new ArgumentNullException("SMS Body value not found in Tempalate configuration item in Sitecore");
                }

                var messageBody = message.GetPropertyValue(Constants.Keys.Body) as string;

                var recipient = GetRecipient(context);
                var toPhoneNumber = recipient.GetPropertyValue(Constants.Keys.PhoneNumber) as string;

                var smsTwilioConfigurationPolicy = context.GetPolicy<SmsTwilioConfigurationPolicy>();

                TwilioClient.Init(smsTwilioConfigurationPolicy.AccountSid, smsTwilioConfigurationPolicy.AuthToken);

                var smsMessage = MessageResource.Create(
                    body: message.GetPropertyValue(Constants.Keys.Body) as string,
                    from: new Twilio.Types.PhoneNumber(smsTwilioConfigurationPolicy.FromPhone),
                    to: new Twilio.Types.PhoneNumber(recipient.GetPropertyValue(Constants.Keys.PhoneNumber) as string)
                );

                Log.Information($"SMS sent to phone number: {toPhoneNumber}, Message: {messageBody}, Message SID: {smsMessage.Sid}");

                return entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error in SendSmsBlock. Message ID: {this.MessageName}, Entity ID: {entity?.Id}");
                return entity;
            }

        }
#pragma warning restore 1998
    }
}
