using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models.AuthModels
{
    public class SignInModel
    {
        [Required(ErrorMessage = "You must enter an email to login")]
        [Display(Order = 0)]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "You must enter a password to login")]
        [Display(Order = 1)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
