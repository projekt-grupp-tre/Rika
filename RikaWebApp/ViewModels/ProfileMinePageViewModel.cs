namespace RikaWebApp.ViewModels;

public class ProfileMinePageViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;
    public string? City { get; set; } = null!;
    public string? PostalCode { get; set; } = null!;
    public string? Country { get; set; } = null!;
    //public int? Age { get; set; }
    public string? ImageUrl { get; set; } = null!;  

}
