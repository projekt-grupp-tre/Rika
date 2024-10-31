using RikaWebApp.ViewModels.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Product
{
    public interface IProductServiceEdvin
    {
        Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(string categoryName);
    }
}
