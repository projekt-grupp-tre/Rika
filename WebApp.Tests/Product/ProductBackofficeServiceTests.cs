using Business.Dto.Product;
using Business.Services.Product.Backoffice;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WebApp.Tests.Product;



public class ProductBackofficeServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly ProductBackofficeService _productBackofficeService;

    public ProductBackofficeServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://productprovidergraphql.azurewebsites.net/")
        };
        _productBackofficeService = new ProductBackofficeService(_httpClient);
    }

   

    [Fact]
    public async Task AddBackofficeProductAsync_AddsProductSuccessfully_WhenResponseIsSuccessful()
    {
        // Arrange
        var productInput = new ProductInputDTO
        {
            Name = "New Product",
            Description = "A new product",
            CategoryName = "Clothes",
            Images = new List<string> { "image1.jpg" },
            Variants = new List<ProductVariantDTO> 
    {
        new ProductVariantDTO { Size = "M", Color = "Red", Stock = 10, Price = 99.99M }
    }
        };

        var jsonResponse = JsonConvert.SerializeObject(new { data = new { addProduct = new { productId = Guid.NewGuid() } } });

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            });

        // Act
        var exception = await Record.ExceptionAsync(() => _productBackofficeService.AddBackofficeProductAsync(productInput));

        // Assert
        Assert.Null(exception); 
    }


}
