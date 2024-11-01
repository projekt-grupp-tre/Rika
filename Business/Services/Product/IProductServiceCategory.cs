using Business.Dto.Product; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Product
{
    public interface IProductServiceCategory
    {
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(string categoryName);

    }
}
