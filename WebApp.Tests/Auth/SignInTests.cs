using Moq.Protected;
using Moq;
using RikaWebApp.Controllers.Auth;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Models.AuthModels;

namespace WebApp.Tests.Auth
{
    public class SignInTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _mockHttpClient;
        private readonly SignInController _controller;
        private readonly Mock<SignInController> _controllerMock;

        public SignInTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);

            var tempData = new Mock<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary>();
            _controller = new SignInController() { TempData = tempData.Object };

            var _controllerMock = new Mock<SignInController>();
        }

        private void SetupHttpResponse(HttpStatusCode statusCode, string content = "")
        {
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                });
        }

        [Fact]
        public async Task SignIn_WhenModelStateIsInvalid_ThenShouldReturnSignInView()
        {
            // Arrange
            var model = new SignInModel { Email = "test@example.com", Password = "" };

            // Act
            var result = await _controller.LoginAsync(model) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SignInView", result.ViewName);
        }
    }
}
