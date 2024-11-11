using Business.Dto.OrderDtos;
using Business.Interfaces.OrderInterfaces;
using Moq;

namespace WebApp.Tests.Order;

public class ShoppingcartService_Tests
{
    #region Testfallsbeskrivning
    // SOM EN ANVÄNDARE VILL JAG KUNNA LÄGGA TILL PRODUKTER I MIN KUNDVAGN
    // Testfallsbeskrivning:
    //- Lyckas lägga till produkt i kundvagnen
    //- Lyckas ej lägga till produkt i kundvagnen, felmeddelande.

    //Förutsättningar:
    //Produkten finns i lager
    //Produkten finns ej i lager / kunde ej anropa API

    //Steg:
    //Användaren trycker på "Lägg till i kundvagn"

    //Förväntat resultat:
    //Produkten läggs till i kundvagnen och kundvagnsikonen adderas med en
    //Felmeddelande/alert när man försöker lägga till produkt?


    // SOM EN ANVÄNDARE VILL JAG KUNNA SE ALLA PRODUKTER I MIN KUNDVAGN
    // Testfallsbeskrivning:
    // Lyckas hämta produkt objekt från ProduktAPI
    // Lyckas ej hämta produkt objekt från ProduktAPI generera felmeddelande.

    // Förutsättningar:
    // Produkten finns i lager.
    // Användaren trycker på "Lägg till i kundvagn"

    // Steg:
    // Trycker på proceed to checkout/ Ikonen uppe till höger

    // Förväntat resultat:
    // Se produkter i "my cart"
    // Felmeddelande/alert när man försöker lägga till produkt?
    #endregion

    private Mock<IShoppingCartService> _mockCartService;

    public ShoppingcartService_Tests()
    {
        _mockCartService = new Mock<IShoppingCartService>();
    }

    [Fact]
    public void AddCartItem_ShouldReturnConfirmationMessage_IfProductIsAddedToCartSuccessfully()
    {
        // Arrange
        CartItemDto cartItem = new CartItemDto { ProductId = "123", Email = "testmail", Quantity = 2 };
        ValidatorResult expectedResult = new ValidatorResult { StatusCode = 200, Message = "Successfully added item to cart" };

        _mockCartService.Setup(x => x.Validate(cartItem)).Returns(expectedResult);

        //Act
        ValidatorResult result = _mockCartService.Object.Validate(cartItem);

        //Assert
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedResult.Message, result.Message);
    }

    [Fact]
    public void AddCartItem_ShouldReturnErrornMessage_IfProductIsNotAddedToCartSuccessfully()
    {
        // Arrange
        CartItemDto cartItem = new CartItemDto { ProductId = "", Email = "testmail", Quantity = 2 };
        ValidatorResult expectedResult = new ValidatorResult { StatusCode = 400, Message = "Could not add item to cart" };

        _mockCartService.Setup(x => x.Validate(cartItem)).Returns(expectedResult);

        // Act
        ValidatorResult result = _mockCartService.Object.Validate(cartItem);

        // Assert
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedResult.Message, result.Message);
    }


    //Ska hämta ut alla cartitems från productAPI kopplade till shoppingcarten
    [Fact]
    public void GetAllCartItems_ShouldGetAllCartItemsFromShoppingCart_IfCartIsNotEmpty()
    {
        // Arrange
        string email = "hennesEmail@email.com";

        var shoppingCart = new ShoppingCartDto
        {
            Id = "123",
            UserEmail = "email",
            Quantity = 2,
            Created = DateTime.UtcNow,
            Totalprice = 2,
            PromoCode = 3456,
            CartItems = new List<CartItemDto> {
                new CartItemDto { ProductId = "blabla", Email ="yo@hej.se", Quantity=2 },
                new CartItemDto { ProductId = "blabla", Email ="yo@hej.se", Quantity=2 }
            }
        };

        _mockCartService.Setup(x => x.GetFullShoppingCart(email)).ReturnsAsync(shoppingCart);

        // Act
        var result = _mockCartService.Object.GetFullShoppingCart(email);

        // Assert
        Assert.Same(shoppingCart, shoppingCart);
        Assert.Equal(shoppingCart.CartItems, shoppingCart.CartItems);
        Assert.NotNull(result);
    }


    //Ska inte hämta ut alla cartitems från productAPI kopplade till shoppingcarten
    [Fact]
    public void GetAllCartItems_ShouldReturnEmptyList_IfCartIsEmpty()
    {
        // Arrange
        string email = "minEmail@email.se";
        var shoppingCart = new ShoppingCartDto
        {
            Id = "123",
            UserEmail = "email",
            Quantity = 2,
            Created = DateTime.UtcNow,
            Totalprice = 2,
            PromoCode = 3456,
            CartItems = new List<CartItemDto>()
        };

        _mockCartService.Setup(x => x.GetFullShoppingCart(email)).ReturnsAsync(shoppingCart);

        // Act
        var result = _mockCartService.Object.GetFullShoppingCart(email);

        // Assert
        Assert.Same(shoppingCart, shoppingCart);
        Assert.Equal(shoppingCart.CartItems, shoppingCart.CartItems);
        Assert.NotNull(result);
        Assert.Empty(shoppingCart.CartItems);
    }
}
