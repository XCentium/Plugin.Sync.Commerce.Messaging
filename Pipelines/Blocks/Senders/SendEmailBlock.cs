using Plugin.Sync.Commerce.Messaging.Models;
using Plugin.Sync.Commerce.Messaging.Pipelines.Arguments;
using Plugin.Sync.Commerce.Messaging.Policies;
using Serilog;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Plugin.Sync.Commerce.Messaging.Pipelines.Blocks
{
    /// <summary>
    /// Sends email via SMTP host
    /// </summary>
    public class SendEmailBlock : PipelineBlock<SendEmailArgument, SendMessageResult, CommercePipelineExecutionContext>
    {
        public override async Task<SendMessageResult> Run(SendEmailArgument arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg, "SendEmailArgument is required").IsNotNull();
            Condition.Requires(arg.MailMessage, "SendEmailArgument.MailMessage is required").IsNotNull();
            Condition.Requires(arg.MailMessage.From, "SendEmailArgument.MailMessage.From is required").IsNotNull();
            Condition.Requires(arg.MailMessage.To, "SendEmailArgument.MailMessage.To is required").IsNotNull();
            Condition.Requires(arg.MailMessage.Subject, "SendEmailArgument.MailMessage.Subject is required").IsNotNull();
            Condition.Requires(arg.MailMessage.Body, "SendEmailArgument.MailMessage.Body is required").IsNotNull();

            try
            {
                var smtpServerPolicy = context.GetPolicy<SmtpConfigurationPolicy>();
                var smtpClient = new SmtpClient
                {
                    Host = smtpServerPolicy.Host,
                    Port = smtpServerPolicy.Port,
                    EnableSsl = smtpServerPolicy.EnableSsl,
                    Credentials = new NetworkCredential(smtpServerPolicy.UserName, smtpServerPolicy.Password)
                };

                await smtpClient.SendMailAsync(arg.MailMessage);
                return new SendMessageResult
                {
                    Success = true
                };
            }
            catch (SmtpFailedRecipientException ex)
            {
                return new SendMessageResult
                {
                    ErrorCode = (int)ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Success = false
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error sending MailMessage: Error: {ex.Message}");
                return new SendMessageResult
                {
                    ErrorCode = -1,
                    ErrorMessage = ex.Message,
                    Success = false
                };
            }
        }
    }
}
