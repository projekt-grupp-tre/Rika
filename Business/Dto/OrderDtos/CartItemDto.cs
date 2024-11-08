
namespace Business.Dto.OrderDtos
{
    public class CartItemDto
    {
        public string Email { get; set; } = null!;

        public string ProductId { get; set; } = null!;
       
        public int Quantity { get; set; }
    }
}
