using Business.Dto.OrderDtos;

namespace Business.Interfaces.OrderInterfaces
{
    public interface IShoppingCartService
    {
        public IEnumerable<ProductDto> GetProctsFromApi();
    }
}
