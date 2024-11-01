namespace RikaWebApp.Models.Communication;
public class ContactViewModel
{
    public string? Title { get; set; }
    public ContactFormModel ContactForm { get; set; } = new();
    public List<ContactServiceModel> Services { get; set; } = new List<ContactServiceModel>
    {
        new ContactServiceModel { Title = "Technical support" },
        new ContactServiceModel { Title = "Legal" },
        new ContactServiceModel { Title = "Product Support" }
    };

    public List<FaQModel> FaQList { get; set; } = new List<FaQModel>();
}
