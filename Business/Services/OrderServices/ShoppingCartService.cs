
using Business.Dto.OrderDtos;
using Business.Interfaces.OrderInterfaces;

namespace Business.Services.OrderServices
{
    public class ShoppingCartService : IShoppingCartService
    {
        public IEnumerable<ProductDto> GetProctsFromApi()
        {
            List<ProductDto> products = new List<ProductDto>();

            for (int i = 0; i < 5; i++)
            {
                products.Add(new ProductDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Dator",
                    Description = "En slö dator",
                    Category = "Datorer",
                    Price = 10000
                });
            }

            return products;

        }


    }
}
