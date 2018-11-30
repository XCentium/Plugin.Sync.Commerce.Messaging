using Sitecore.Commerce.Core;

namespace XCentium.Sitecore.Commerce.Messages.Policies
{
    /// <summary>
    /// SMTP Server configuration
    /// </summary>
    public class SmtpConfigurationPolicy : Policy
    {
        public SmtpConfigurationPolicy()
        {
        }

        public string FromEmailAddress { get; set; }
        public string FromEmailDisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
