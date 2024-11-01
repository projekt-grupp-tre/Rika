public class ProductModel
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public CategoryModel Category { get; set; }
    public List<VariantModel> Variants { get; set; }
    public List<ReviewModel> Reviews { get; set; }
}

public class CategoryModel
{
    public string Name { get; set; }
}

public class VariantModel
{
    public string Size { get; set; }
    public string Color { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}

public class ReviewModel
{
    public string ClientName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}