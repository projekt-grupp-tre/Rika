using Business.Dto.Product;

namespace RikaWebApp.ViewModels;

public class ProductViewModel
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<string> Images { get; set; } = [];

    //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public virtual CategoryViewModel? Category { get; set; }
    public virtual ICollection<ProductVariantViewModel>? Variants { get; set; }
    public virtual ICollection<ReviewViewModel>? Reviews { get; set; }



    public static ProductViewModel FromDto(ProductDTO productDto)
    {
        return new ProductViewModel
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            Description = productDto.Description,
            Images = productDto.Images,
            Category = new CategoryViewModel
            {
                Name = productDto.Category.Name
            },
            Variants = productDto.Variants.Select(v => new ProductVariantViewModel
            {
                Size = v.Size,
                Color = v.Color,
                Stock = v.Stock,
                Price = v.Price
            }).ToList(),
            Reviews = productDto.Reviews.Select(r => new ReviewViewModel
            {
                ClientName = r.ClientName,
                Rating = r.Rating,
                Comment = r.Comment
            }).ToList()
        };
    }
}

