using Serilog;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Management;
using Sitecore.Framework.Pipelines;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using XCentium.Sitecore.Commerce.Messages.Policies;
using XCentium.Sitecore.Commerce.Messages.Shared;

namespace XCentium.Sitecore.Commerce.Messages.Pipelines.Blocks
{
    /// <summary>
    /// Sends email via SMTP host
    /// </summary>
    public class SendEmailBlock : PipelineMessageBlockBase
    {
#pragma warning disable 1998
        public override async Task<CommerceEntity> Run(CommerceEntity entity, CommercePipelineExecutionContext context)
        {
            try
            {
                var message = GetMessage(context);

                if (!message.ContainsProperty(Constants.Keys.Subject) || string.IsNullOrEmpty(message.GetPropertyValue(Constants.Keys.Subject) as string))
                {
                    throw new ArgumentNullException("Email Subject value not found in Email Tempalte configuration item in Sitecore");
                }

                if (!message.ContainsProperty(Constants.Keys.Body) || string.IsNullOrEmpty(message.GetPropertyValue(Constants.Keys.Body) as string))
                {
                    throw new ArgumentNullException("Email Body value not found in Email Tempalte configuration item in Sitecore");
                }

                var smtpServerPolicy = context.GetPolicy<SmtpConfigurationPolicy>();
                var smtpClient = new SmtpClient
                {
                    Host = smtpServerPolicy.Host,
                    Port = smtpServerPolicy.Port,
                    EnableSsl = smtpServerPolicy.EnableSsl,
                    Credentials = new NetworkCredential(smtpServerPolicy.UserName, smtpServerPolicy.Password)
                };

                var recipient = GetRecipient(context);
                var fromAddress = string.IsNullOrEmpty(smtpServerPolicy.FromEmailDisplayName) ? new MailAddress(smtpServerPolicy.FromEmailAddress) : new MailAddress(smtpServerPolicy.FromEmailAddress, smtpServerPolicy.FromEmailDisplayName);
                var toAddress = new MailAddress((string)recipient.GetPropertyValue(Constants.Keys.To));

                using (var mailMessage  = new MailMessage(fromAddress, toAddress)
                {
                    Subject = message.GetPropertyValue(Constants.Keys.Subject) as string,
                    Body = message.GetPropertyValue(Constants.Keys.Body) as string
                })
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error in SendEmailBlock. Message ID: {this.MessageName}, Entity ID: {entity?.Id}");
            }

            return entity;
        }
#pragma warning restore 1998
    }
}
