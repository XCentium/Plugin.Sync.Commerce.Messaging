using Sitecore.Commerce.Core;

namespace Plugin.Sync.Commerce.Messaging.Policies
{
    /// <summary>
    /// SMTP Server configuration
    /// </summary>
    public class SmsTwilioConfigurationPolicy : Policy
    {
        public SmsTwilioConfigurationPolicy()
        {
        }

        /// <summary>
        /// Twilio Account SID (user name)
        /// </summary>
        public string AccountSid { get; set; }

        /// <summary>
        /// Twilio Authentication token (password)
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// From phone number to apply on outgoing text messages
        /// </summary>
        public string FromPhone { get; set; }
    }
}
