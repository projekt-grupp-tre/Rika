namespace RikaWebApp.Models.Communication
{
    public class MessageModel
    {
				public string UserName { get; set; } = null!;
        public string SenderUserId { get; set; } = null!;
				public string? GroupId { get; set; }
        public string MessageContent { get; set; } = null!;
        public DateTime MessageSentAt = DateTime.Now;
        public DateTime? MessageReceivedAt {  get; set; }
				public List<string>? AttachmentUrls { get; set; }
    }
}
