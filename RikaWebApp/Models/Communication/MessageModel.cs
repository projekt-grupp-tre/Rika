namespace RikaWebApp.Models.Communication
{
    public class MessageModel
    {
        public string SenderUserId { get; set; } = null!;
        public bool IsSentFromThisClient { get; set; }
        public string Content { get; set; } = null!;
        public DateTime MessageSentAt = DateTime.Now;
        public DateTime? MessageReceivedAt {  get; set; }
    }
}
