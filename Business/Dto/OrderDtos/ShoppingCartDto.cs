
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Dto.OrderDtos
{
    public class ShoppingCartDto
    {
        public string Id { get; set; } = null!;

        public string UserEmail { get; set; } = null!; 

        public int Quantity { get; set; } 

        public decimal Totalprice { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public List<CartItemDto>? CartItems { get; set; } = new List<CartItemDto>();

        public int PromoCode { get; set; }
    }
}
