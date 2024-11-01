namespace RikaWebApp.Models.Communication;
public class ContactSubmissionModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ServiceCategory { get; set; }
    public string Message { get; set; } = null!;
}