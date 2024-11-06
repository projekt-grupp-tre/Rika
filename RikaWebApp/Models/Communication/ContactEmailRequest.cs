namespace RikaWebApp.Models.Communication;
public class ContactEmailRequest
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string HtmlBody { get; set; } = null!;
    public string PlainText { get; set; } = null!;
}