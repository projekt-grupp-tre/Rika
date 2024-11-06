using Business.Dto.Product;
using Business.Services.Product;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;
namespace WebApp.Tests.Product;

public class ProductServiceCategoryTests
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;

    public ProductServiceCategoryTests()
    {
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
    }

    [Fact]
    public async Task GetProductsByCategoryAsync_Should_ReturnProductsInCategory()
    {
        // Arrange
        var categoryName = "Electronics";
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(new
            {
                data = new
                {
                    getProductsByCategory = new[]
                    {
                            new
                            {
                                productId = Guid.NewGuid().ToString(),
                                name = "Laptop",
                                description = "A high-performance laptop",
                                images = new[] { "image1.jpg", "image2.jpg" },
                                category = new { name = categoryName },
                                variants = new[]
                                {
                                    new { size = "15 inch", color = "Black", stock = 10, price = 999.99m }
                                },
                                reviews = new[]
                                {
                                    new { clientName = "John Doe", rating = 5, comment = "Excellent product!" }
                                }
                            }
                        }
                }
            }), Encoding.UTF8, "application/json")
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);
        _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var productService = new ProductServiceCategory(_mockHttpClientFactory.Object);

        // Act
        var result = await productService.GetProductsByCategoryAsync(categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        var product = result.First();
        Assert.Equal("Laptop", product.Name);
        Assert.Equal("A high-performance laptop", product.Description);
        Assert.Equal("Electronics", product.Category.Name);
        Assert.NotEmpty(product.Variants);
        Assert.NotEmpty(product.Reviews);
    }

    [Fact]
    public async Task GetProductsByCategoryAsync_Should_ThrowException_WhenApiReturnsServerError()
    {
        // Arrange
        var categoryName = "Electronics";
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.InternalServerError
        };

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);
        _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var productService = new ProductServiceCategory(_mockHttpClientFactory.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
            productService.GetProductsByCategoryAsync(categoryName)
        );

        Assert.Contains("Request failed with status code", exception.Message);
    }

}
