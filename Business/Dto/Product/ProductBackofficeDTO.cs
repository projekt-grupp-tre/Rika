namespace Business.Dto.Product;

public record ProductBackofficeDTO
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public CategoryDTO Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
