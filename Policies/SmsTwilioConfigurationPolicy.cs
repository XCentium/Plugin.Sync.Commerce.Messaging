using Sitecore.Commerce.Core;

namespace XCentium.Sitecore.Commerce.Messages.Policies
{
    /// <summary>
    /// SMTP Server configuration
    /// </summary>
    public class SmsTwilioConfigurationPolicy : Policy
    {
        public SmsTwilioConfigurationPolicy()
        {
        }

        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string FromPhone { get; set; }
    }
}
