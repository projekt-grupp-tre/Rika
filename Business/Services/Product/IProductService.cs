using Business.Dto.Product;

namespace Business.Services.Product
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductById(Guid productId);
    }
}