using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "A name is required")]
        [Display(Order = 0)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "A name is required")]
        [Display(Order = 1)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "An email is required")]
        [Display(Order = 2)]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A password is required")]
        [Display(Order = 3)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.[a-z])(?=.[A-Z])(?=.\\d)(?=.[!@#$%^&*()\\-_=+{};:,<.>]).{8,}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "You must confirm your password")]
        [Display(Order = 4)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Order = 5)]
        [CheckBoxRequired(ErrorMessage = "You must agree to our terms and condition")]
        public bool TermsAndConditions { get; set; } = false;
    }

    public class CheckBoxRequired : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is bool b && b;
        }
    }
}
