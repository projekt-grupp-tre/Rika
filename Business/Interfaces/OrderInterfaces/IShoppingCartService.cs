using Business.Dto.OrderDtos;

namespace Business.Interfaces.OrderInterfaces
{
    public interface IShoppingCartService
    {
        //Task<ProductDto> GetOneProductAsync();

        //Task<string?> GetOneProductByIdAsync(int productId);

        ValidatorResult Validate(CartItemDto cartItemDto);

        Task<ShoppingCartDto> GetFullShoppingCart(string email);

        Task<GraphQLResponse> GetAllCartItemsFromCart(List<string> ids);
    }
}
