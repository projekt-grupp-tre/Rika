using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.ViewModels
{
    public class ProductVariantViewModel
    {
        public Guid ProductVariantId { get; set; }

        public string Size { get; set; } = null!;

        public string Color { get; set; } = null!;

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public virtual ProductViewModel Product { get; set; } = null!;
    }
}