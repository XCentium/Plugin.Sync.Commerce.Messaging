using Plugin.Sync.Commerce.Messaging.Models;
using Sitecore.Commerce.Core;

namespace Plugin.Sync.Commerce.Messaging.Pipelines.Arguments
{
    /// <summary>
    /// Send SMS pipeline argument with SMS message model
    /// </summary>
    public class SendTwilioSmsArgument : PipelineArgument
    {
        public TwilioSmsMessage SmsMessage { get; set; }
        public SendTwilioSmsArgument(TwilioSmsMessage smsMessage)
        {
            this.SmsMessage = smsMessage;
        }
    }
}
