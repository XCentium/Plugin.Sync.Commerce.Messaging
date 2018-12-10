using Sitecore.Commerce.Core;

namespace XCentium.Sitecore.Commerce.Messages.Policies
{
    /// <summary>
    /// SMTP Server configuration for SendEmailBlock
    /// </summary>
    public class SmtpConfigurationPolicy : Policy
    {
        public SmtpConfigurationPolicy()
        {
        }

        /// <summary>
        /// From email address to be set on all outgoing emails
        /// </summary>
        public string FromEmailAddress { get; set; }

        /// <summary>
        /// Display name for "From" semail address
        /// </summary>
        public string FromEmailDisplayName { get; set; }

        /// <summary>
        /// SMTP Servert host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// SMTP server port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// When set to true then use SSL when sending emails via SMTP server
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Password to use for SMTP server connection
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User Name to use for SMTP server connection
        /// </summary>
        public string UserName { get; set; }
    }
}
