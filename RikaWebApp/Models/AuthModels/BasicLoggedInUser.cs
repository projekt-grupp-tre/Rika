namespace RikaWebApp.Models.AuthModels
{
    public class BasicLoggedInUser
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? ImageUrl { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
    }
}
