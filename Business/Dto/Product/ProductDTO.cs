namespace Business.Dto.Product;

public class ProductDTO
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public CategoryDTO Category { get; set; }
    public List<VariantDTO> Variants { get; set; }
    public List<ReviewDTO> Reviews { get; set; }
}
