using Business.Services.Product;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Tests.Product;

public class ProductReviewServiceTests
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;

    public ProductReviewServiceTests()
    {
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
    }

    [Fact]
    public async Task GetReviewsByProductIdAsync_Should_ReturnReviews()
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
                        reviews = new[]
                        {
                            new { clientName = "John Doe", rating = 5, comment = "Great product!", createdAt = DateTime.UtcNow },
                            new { clientName = "Jane Doe", rating = 4, comment = "Good quality!", createdAt = DateTime.UtcNow }
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

        var productReviewService = new ProductReviewService(_mockHttpClientFactory.Object);

        // Act
        var result = await productReviewService.GetReviewsByProductIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("John Doe", result.First().ClientName);
        Assert.Equal(5, result.First().Rating);
        Assert.Equal("Great product!", result.First().Comment);
    }

    [Fact]
    public async Task GetReviewsByProductIdAsync_Should_ThrowException_WhenApiReturnsServerError()
    {
        // Arrange
        var productId = Guid.NewGuid();
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

        var productReviewService = new ProductReviewService(_mockHttpClientFactory.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
            productReviewService.GetReviewsByProductIdAsync(productId)
        );

        Assert.Contains("Request failed with status code", exception.Message);
    }

    [Fact]
    public async Task GetReviewsByProductIdAsync_Should_ReturnEmptyList_WhenNoReviewsFound()
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
                        reviews = new object[] { }
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

        var productReviewService = new ProductReviewService(_mockHttpClientFactory.Object);

        // Act
        var result = await productReviewService.GetReviewsByProductIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
