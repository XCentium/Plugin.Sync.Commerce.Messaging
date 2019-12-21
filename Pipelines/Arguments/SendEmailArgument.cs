using Sitecore.Commerce.Core;
using System.Net.Mail;

namespace Plugin.Sync.Commerce.Messaging.Pipelines.Arguments
{
    /// <summary>
    /// SendEmail pipeline argument with Email message model
    /// </summary>
    public class SendEmailArgument : PipelineArgument
    {
        public MailMessage MailMessage { get; set; }
        public SendEmailArgument(MailMessage mailMessage)
        {
            this.MailMessage = mailMessage;
        }
    }
}
