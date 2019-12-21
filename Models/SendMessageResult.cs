namespace Plugin.Sync.Commerce.Messaging.Models
{
    public class SendMessageResult
    {
        public string ErrorMessage { get; set; }
        public int? ErrorCode { get; set; }
        public bool Success { get; set; }
    }
}
