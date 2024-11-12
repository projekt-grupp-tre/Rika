using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.ViewModels;

public class ProfileUpdateViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "First name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    [MinLength(2, ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    [MinLength(2, ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email address", Prompt = "Email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email address")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$", ErrorMessage = "invalid email")]
    public string Email { get; set; } = null!;


    [DataType(DataType.Text)]
    [Display(Name = "Address", Prompt = "Street name", Order = 3)]
    [Required(ErrorMessage = "Invalid address")]
    [MinLength(2, ErrorMessage = "Invalid address")]
    public string Address { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "City", Order = 4)]
    [Required(ErrorMessage = "Invalid City name")]
    [MinLength(2, ErrorMessage = "Invalid City name")]
    public string City { get; set; } = null!;


    [DataType(DataType.Text)]
    [Display(Name = "PostalCode", Prompt = "PostalCode", Order = 5)]
    [Required(ErrorMessage = "Invalid PostalCode")]
    [MinLength(5, ErrorMessage = "Invalid PostalCode")]
    public string PostalCode { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Country", Prompt = "Country", Order = 6)]
    [Required(ErrorMessage = "Invalid Country")]
    [MinLength(3, ErrorMessage = "Invalid Country")]
    public string Country { get; set; } = null!;


    //[DataType(DataType.Text)]
    //[Display(Name = "Age", Prompt = "Age", Order = 7)]
    //[Required(ErrorMessage = "Invalid Age")]
    //[MinLength(3, ErrorMessage = "Invalid Age")]
    //public string Age { get; set; } = null!;




    [DataType(DataType.Text)]
    [Display(Name = "ImageUrl", Prompt = "Profile Image", Order = 8)]
    [Required(ErrorMessage = "Invalid Profile ImageUrl")]
    [MinLength(2, ErrorMessage = "Invalid Profile ImageUrl")]
    public string ImageUrl { get; set; } = null!;

}











    
