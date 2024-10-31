namespace RikaWebApp.ViewModels.Product;

public class ProductIndexViewModel
{
    public string ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> Images { get; set; } = [];
    public decimal Price { get; set; }

}
