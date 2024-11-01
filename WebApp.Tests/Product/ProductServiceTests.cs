using Business.Dto.Product;
using Business.Services.Product;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace WebApp.Tests.Product;

public class ProductServiceTests
{
    private readonly Mock<IProductService> _mockProductService;

    public ProductServiceTests()
    {
        _mockProductService = new Mock<IProductService>();
    }

    [Fact]
    public async Task GetAllProductsAsync_Should_ReturnListOfProducts()
    {
        //arrange
        var products = new List<ProductDTO>
        {
            new()
            {
                ProductId = Guid.NewGuid(),
                Name = "Tröja",
                Description = "En skön tröja",
                Variants = new List<VariantDTO>
                {
                    new VariantDTO { Price = 199.99m, Color = "Röd", Size = "M", Stock = 3 },
                    new VariantDTO { Price = 499.99m, Color = "Blå", Size = "s", Stock = 30 },
                    new VariantDTO { Price = 499.99m, Color = "Röd", Size = "M", Stock = 0 },
                },
                Reviews = new List<ReviewDTO>
                {
                    new ReviewDTO { ClientName = "TestPerson 1", Comment = "Skön tröja", Rating = 4 },
                    new ReviewDTO { ClientName = "TestPerson 2", Comment = "Trist tröja", Rating = 1 },
                },
                Category = new CategoryDTO
                {
                    Name = "Tröjor"
                },

            },
            new()
            {
                ProductId = Guid.NewGuid(),
                Name = "Hörlurar",
                Description = "Hörlurar från marshall",
                Variants = new List<VariantDTO>
                {
                    new VariantDTO { Price = 1999.99m, Color = "Röd", Size = "M", Stock = 0 },
                    new VariantDTO { Price = 499.99m, Color = "Blå", Size = "s", Stock = 20 },
                    new VariantDTO { Price = 899.99m, Color = "Gul", Size = "L", Stock = 2 },
                },
                Reviews = new List<ReviewDTO>
                {
                    new ReviewDTO { ClientName = "TestPerson 1", Comment = "Sköna lurar", Rating = 4 },
                    new ReviewDTO { ClientName = "TestPerson 2", Comment = "Dåliga lurar", Rating = 1 },
                },
                Category = new CategoryDTO
                {
                    Name = "Elektronik"
                },
            }
        };

        _mockProductService.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(products);

        //Act
        var result = await _mockProductService.Object.GetAllProductsAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Tröja", result[0].Name);
        Assert.Equal("Hörlurar", result[1].Name);
        
        var variants = result[0].Variants;
        Assert.Equal(3, variants[0].Stock);
        Assert.Equal(0, variants[2].Stock);
        Assert.Equal(30, variants[1].Stock);

        Assert.True(variants[0].Stock > 0, "produkten finns i lager");
        Assert.True(variants[1].Stock > 0, "produkten finns i lager");
        Assert.True(variants[2].Stock == 0, "produkten finns inte i lager");
    }



    [Fact]
    public async Task GetProductById_Should_ReturnProductWhenExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new ProductDTO
        {
            ProductId = productId,
            Name = "Produkt 1",
            Description = "Beskrivning för produkt 1",
            Reviews = new List<ReviewDTO>
            {
                new ReviewDTO { ClientName = "TestPerson 1", Comment = "Dålig produkt", Rating = 1 },
                new ReviewDTO { ClientName = "TestPerson 2", Comment = "Helt okej", Rating = 3 },
                new ReviewDTO { ClientName = "TestPerson 3", Comment = "Fantastisk!", Rating = 5 }
            }
        };

        _mockProductService.Setup(service => service.GetProductById(productId)).ReturnsAsync(product);

        // Act
        var result = await _mockProductService.Object.GetProductById(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Produkt 1", result.Name);
        Assert.Equal("Beskrivning för produkt 1", result.Description);

        Assert.Equal(3, result.Reviews.Count);
        var averageRating = product.Reviews.Average(r => r.Rating);
        Assert.Equal(averageRating, result.Reviews.Average(r => r.Rating));
    }
}
