
namespace Business.Dto.OrderDtos
{
    public class CartItemDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public int ProductId { get; set; }
       
        //public int ShoppingCartId { get; set; }

        public int Quantity { get; set; }

        //public DateTime DateTime { get; set; }
    }
}
