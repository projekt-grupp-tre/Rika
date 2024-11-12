namespace Business.Dto.OrderDtos;

public class ProductListResponse
{
    public List<ProductDto> Products { get; set; }

    public ProductListResponse()
    {
        Products = new List<ProductDto>();
    }
}
