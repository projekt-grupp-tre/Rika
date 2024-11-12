namespace Business.Dto.Product;

public class VariantDTO
{
    public Guid ProductVariantId { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}