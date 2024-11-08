using Business.Dto.OrderDtos;

namespace Business.Interfaces.OrderInterfaces
{
    public interface IShoppingCartService
    {
        Task<ResponseDto> AddProductToCartAsync(string email, string productId);

        Task<ProductDto> GetOneProductAsync();

        Task<string?> GetOneProductByIdAsync(int productId);

        Task<string> GetUserByEmailAsync(string email);

        Task<bool> SendCartItemAsync(string email, string productId);

        ValidatorResult Validate(CartItemDto cartItemDto);

        Task<ShoppingCartDto> GetFullShoppingCart(string email);
    }
}
