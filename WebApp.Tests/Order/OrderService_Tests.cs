using Business.Dto.OrderDtos;
using Business.Interfaces.OrderInterfaces;
using Moq;

namespace WebApp.Tests.Order;

public class OrderService_Tests
{

    private Mock<IOrderValidator> _mockOrderValidator;
    private Mock<IOrderService> _mockOrderService;

    public OrderService_Tests()
    {
        _mockOrderValidator = new Mock<IOrderValidator>();
        _mockOrderService = new Mock<IOrderService>();
    }



    [Fact]
    public async Task CreateOrder_Should_Return_ComfirmationMessageIfAllOrderFieldIsValid()
    {
        // Arrange
        var order = new OrderDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            Phone = "1234567890",
            DeliveryAddress = "123 Example St",
            Products = new List<ProductDto>
            {
                new ProductDto { Name = "Product 1", Description = "Description 1", Price = 10.00 }
            },
            ShippingMethod = "Standard",
            PaymentMethod = "Credit Card",
            TotalPrice = 120
        };


        ValidatorResult expectedValidatorResult = new ValidatorResult { StatusCode = 200 };

        ServiceResult expectedServiceResult = new ServiceResult { StatusCode = 200, Message = "Order har lagt till framgångsrikt" };

        _mockOrderValidator.Setup(x => x.Validate(order)).Returns(expectedValidatorResult);
        _mockOrderService.Setup(x => x.SaveOrderAsync(It.IsAny<OrderDto>())).ReturnsAsync(expectedServiceResult);


        // Act
        var result = await _mockOrderService.Object.SaveOrderAsync(order);

        // Assert
        Assert.Equal(expectedServiceResult, result);
        Assert.Equal(expectedServiceResult.Message, result.Message);


    }


    [Fact]

    public void CreateOrder_Should_Return_SaveOrderData()
    {
        // Arrange
        var order = new OrderDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            Phone = "1234567890",
            DeliveryAddress = "123 Example St",
            Products = new List<ProductDto>
            {
                new ProductDto { Name = "Product 1", Description = "Description 1", Price = 10.00 }
            },
            ShippingMethod = "Standard",
            PaymentMethod = "Credit Card",
            TotalPrice = 120
        };

        ValidatorResult expectedResult = new ValidatorResult { StatusCode = 200, Message = "Sparning av data lyckas." };

        _mockOrderValidator.Setup(x => x.Validate(order)).Returns(expectedResult);


        // Act
        ValidatorResult result = _mockOrderValidator.Object.Validate(order);

        // Assert
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedResult.Message, result.Message);
    }



    [Fact]
    public void CreateOrder_Should_Return_FailureResult_WhenSaveFails()
    {
        // Arrange
        var order = new OrderDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            Phone = "1234567890",
            DeliveryAddress = "123 Example St",
            Products = new List<ProductDto>
        {
            new ProductDto { Name = "Product 1", Description = "Description 1", Price = 10.00 }
        },
            ShippingMethod = "Standard",
            PaymentMethod = "Credit Card",
            TotalPrice = 120
        };


        ValidatorResult expectedFailureResult = new ValidatorResult
        {
            StatusCode = 500,
            Message = "Failed to save to database."
        };


        _mockOrderValidator.Setup(x => x.Validate(order)).Returns(expectedFailureResult);

        // Act
        ValidatorResult result = _mockOrderValidator.Object.Validate(order);

        // Assert
        Assert.Equal(expectedFailureResult, result);
        Assert.Equal(expectedFailureResult.Message, result.Message);
        Assert.Equal(500, result.StatusCode);
    }


    [Fact]
    public void CreateOrder_Should_Return_FailureResult_WhenSaveFails_IfPriceIsInvalid()
    {
        // Arrange
        var order = new OrderDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            Phone = "1234567890",
            DeliveryAddress = "123 Example St",
            Products = new List<ProductDto>
        {
            new ProductDto { Name = "Product 1", Description = "Description 1", Price = -10 }
        },
            ShippingMethod = "Standard",
            PaymentMethod = "Credit Card",
            TotalPrice = 120
        };


        ValidatorResult expectedFailureResult = new ValidatorResult
        {
            StatusCode = 400,
            Message = "Price must be a positive number."
        };


        _mockOrderValidator.Setup(x => x.Validate(order)).Returns(expectedFailureResult);

        // Act
        ValidatorResult result = _mockOrderValidator.Object.Validate(order);

        // Assert
        Assert.Equal(expectedFailureResult, result);
        Assert.Equal(expectedFailureResult.Message, result.Message);
        Assert.Equal(400, result.StatusCode);
    }



    [Fact]
    public void ValidatePaymentMethod_Should_ReturnSuccess_WhenPaymentMethodIsValid()
    {
        // Arrange
        var validPaymentMethod = "Credit Card";
        var expectedResult = new ValidatorResult
        {
            StatusCode = 200,
            Message = "Valid payment method."
        };


        _mockOrderValidator = new Mock<IOrderValidator>();


        _mockOrderValidator
            .Setup(v => v.ValidatePaymentMethod(validPaymentMethod))
            .Returns(expectedResult);

        // Act
        var result = _mockOrderValidator.Object.ValidatePaymentMethod(validPaymentMethod);

        // Assert
        Assert.Equal(expectedResult.StatusCode, result.StatusCode);
        Assert.Equal(expectedResult.Message, result.Message);
    }


    [Fact]
    public void ValidatePaymentMethod_Should_ReturnFailure_WhenPaymentMethodIsInvalid()
    {
        // Arrange
        var invalidPaymentMethod = "Bitcoin";
        var expectedResult = new ValidatorResult
        {
            StatusCode = 400,
            Message = "Invalid payment method."
        };


        _mockOrderValidator = new Mock<IOrderValidator>();


        _mockOrderValidator
            .Setup(v => v.ValidatePaymentMethod(invalidPaymentMethod))
            .Returns(expectedResult);

        // Act
        var result = _mockOrderValidator.Object.ValidatePaymentMethod(invalidPaymentMethod);

        // Assert
        Assert.Equal(expectedResult.StatusCode, result.StatusCode);
        Assert.Equal(expectedResult.Message, result.Message);
    }

}
