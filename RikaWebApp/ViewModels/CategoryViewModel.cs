using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;


        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<ProductViewModel>? Products { get; set; }
    }
}