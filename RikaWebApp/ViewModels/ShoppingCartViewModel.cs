using Business.Dto.OrderDtos;
using Business.Dto.Product;
using Business.Services.Product;
using RikaWebApp.Models.OrderModels;

namespace RikaWebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItemDto>? CartItemDtos { get; set; } = new List<CartItemDto>();
        public GraphQLResponse ProductResponse { get; set; } = new GraphQLResponse();

        public ProductDTO? Product { get; set; }

        public int Id { get; set; } 

        public string? Email { get; set; }

        public PromoCodeFormModel PromoCodeForm { get; set; } = null!;
    }
}
