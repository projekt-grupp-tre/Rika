using Business.Dto.OrderDtos;
using Business.Dto.Product;
using Business.Services.Product;

namespace Business.Interfaces.OrderInterfaces
{
    public interface IShoppingCartService
    {
        Task<ResponseDto> AddProductToCartAsync(string email, string productId);

        Task<ProductDto> GetOneProductAsync();

        Task<string?> GetOneProductByIdAsync(int productId);

        //Task<string> GetUserByEmailAsync(string email);

        Task<bool> SendCartItemAsync(string email, string productId);

        ValidatorResult Validate(CartItemDto cartItemDto);

        Task<ShoppingCartDto> GetFullShoppingCart(string email);

        Task<GraphQLProductListResponse> GetAllCartItemsFromCart(List<string> ids);
    }
}
