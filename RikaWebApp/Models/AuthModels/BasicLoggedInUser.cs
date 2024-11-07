namespace RikaWebApp.Models.AuthModels
{
    public class BasicLoggedInUser
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName{ get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
