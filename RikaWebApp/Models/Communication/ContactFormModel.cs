using System.ComponentModel.DataAnnotations;
using RikaWebApp.Helpers;

namespace RikaWebApp.Models.Communication;

public class ContactFormModel
{
	  [DataType(DataType.Text)]
    [Display(Name = "Full name", Prompt = "Enter your full name")]
    [Required(ErrorMessage = "Full name required")]
    [MinLength(2, ErrorMessage = "Name too short")]
    [MaxLength(50, ErrorMessage = "Name too long")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Valid email required")]
    [Display(Name = "Email", Prompt = "Enter your email address")]
    [RegularExpression("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))\\z", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Service", Prompt = "Choose the service you're interested in")]
    public string? ContactService { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Message", Prompt = "Enter your message here...")]
    [Required(ErrorMessage = "Message required")]
    [MinLength(8, ErrorMessage = "Message too short")]
    [MaxLength(300, ErrorMessage = "Message too long")]
    public string Message { get; set; } = null!;

		[CheckBoxRequired(ErrorMessage = "Checkbox is required")]
		[Display(Name = "I Accept the Terms and Conditions")]
    public bool TermsAndConditions { get; set; } = false;
}