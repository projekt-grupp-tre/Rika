using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models.OrderModels
{
    public class PromoCodeFormModel
    {
        [Display(Name="Promocode", Prompt = "Enter code")]
        [MinLength(5, ErrorMessage = "Minlength 5 characters"), MaxLength(5, ErrorMessage ="Maxlength 5 characters")]
        [DataType(DataType.Text)]    
        public string? Code { get; set; }
    }
}
