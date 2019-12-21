namespace Plugin.Sync.Commerce.Messaging.Models
{
    /// <summary>
    /// Twilio SMS text Mesage fields
    public class TwilioSmsMessage
    {
        /// <summary>
        /// From phone number to be shown in received message
        /// </summary>
        public string FromPhoneNumber { get; set; }

        /// <summary>
        /// Message recipient phone number
        /// </summary>
        public string ToPhoneNumber { get; set; }

        /// <summary>
        /// SMS text messsage body
        /// </summary>
        public string MessageBody { get; set; }

        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="fromPhoneNumber">From phone number to be shown in received message</param>
        /// <param name="toPhoneNumber">Message recipient phone number</param>
        /// <param name="messageBody">SMS text messsage body</param>
        public TwilioSmsMessage(string fromPhoneNumber, string toPhoneNumber, string messageBody)
        {
            this.FromPhoneNumber = fromPhoneNumber;
            this.ToPhoneNumber = toPhoneNumber;
            this.MessageBody = messageBody;
        }
    }
}
