using Business.Dto.OrderDtos;
using RikaWebApp.Models.OrderModels;

namespace RikaWebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ProductDto>? Products { get; set; } = [];

        public ProductDto? Product { get; set; }

        public int Id { get; set; }  //kanske

        public string? Email { get; set; }

        public PromoCodeFormModel PromoCodeForm { get; set; } = null!;
    }
}
