using Business.Services.Product;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace WebApp.Tests.Product;

public class ProductServiceTests
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;

    public ProductServiceTests()
    {
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
    }

    [Fact]
    public async Task GetAllProductsAsync_Should_ReturnListOfProducts()
    {
        // Arrange
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(new
            {
                data = new
                {
                    getProducts = new[]
                    {
                    new { productId = Guid.NewGuid().ToString(), name = "Product 1" }, 
                    new { productId = Guid.NewGuid().ToString(), name = "Product 2" }
                }
                }
            }))
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
        _mockHttpClientFactory.Setup(_ => _.CreateClient("AzureFunctionClient")).Returns(httpClient);

        var productService = new ProductService(_mockHttpClientFactory.Object);

        // Act
        var result = await productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Product 1", result[0].Name);
        Assert.Equal("Product 2", result[1].Name);
    }



    [Fact]
    public async Task GetProductById_Should_ReturnProductWhenExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(new
            {
                data = new
                {
                    getProductById = new
                    {
                        productId = productId.ToString(),
                        name = "Product 1",
                        description = "A great product"
                    }
                }
            }))
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
        _mockHttpClientFactory.Setup(_ => _.CreateClient("AzureFunctionClient")).Returns(httpClient);

        var productService = new ProductService(_mockHttpClientFactory.Object);

        // Act
        var result = await productService.GetProductById(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Product 1", result.Name);
        Assert.Equal("A great product", result.Description);
    }
}
