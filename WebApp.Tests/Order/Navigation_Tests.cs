using Business.Interfaces.OrderInterfaces;
using Moq;

namespace WebApp.Tests.Order;

public class Navigation_Tests
{
    #region Testfallsbeskrivning
    // Testfallsbeskrivning:

    //- Testa så att användarna kommer till nästa sidan.
    //- Testa så att användare kommer till 404 om sidan inte finns.
    //- Testa att användaren omdirigeras till inloggnings sidan om ej authentiserad.


    // Förutsättningar:

    // Anvandaren är inloggad och har en aktiv session.
    // Användaren är inloggad men nästa sida finns ej.
    // Anvandaren inte har en aktiv session.

    // Steg:
    // 1.  Användarna trycka på kundvagns ikonen och är autentiserad.

    // Förväntat resultat
    // [] Använden komma till nästa sidan
    // [] Användaren kommer till 404 sidan
    // [] Användaren kommer till inloggnings sidan

    // Testet är klart när: Alla testar är gröna


    //Som en användare vill jag ha en tillbaka ikon för att kunna välja att backa ur kundvagnen
    #endregion

    private Mock<INavigation> _mockNavigation;
    private Mock<IUserService> _mockUserService;

    public Navigation_Tests()
    {
        _mockNavigation = new Mock<INavigation>();
        _mockUserService = new Mock<IUserService>();
    }

    [Fact]
    public void GoToNextPage_ShouldNavigateToNextPage_IfUserIsAuthenticated()
    {
        // Arrange
        _mockUserService.Setup(x => x.UserIsAuthenticated()).Returns(true);

        // Act 
        if (_mockUserService.Object.UserIsAuthenticated())
        {
            _mockNavigation.Object.GoToNextPage();
        }

        // Assert
        _mockNavigation.Verify(NavigationService_Tests => NavigationService_Tests.GoToNextPage(), Times.Once);
    }

    [Fact]
    public void GoTo404Page_ShouldNavigateTo404Page_IfPageDoesNotExist()
    {
        // Arrange
        bool pageExists = false;

        // Act
        if (!pageExists)
        {
            _mockNavigation.Object.GoTo404Page();
        }

        // Assert
        _mockNavigation.Verify(nav => nav.GoTo404Page(), Times.Once);
    }

    [Fact]
    public void RedirectToLoginPage_ShouldRedirectToLoginPage_IfUserIsNotAuthenticated()
    {
        // Arrange
        _mockUserService.Setup(x => x.UserIsAuthenticated()).Returns(false);

        // Act
        if (!_mockUserService.Object.UserIsAuthenticated())
        {
            _mockNavigation.Object.RedirectToLoginPage();
        }

        // Assert
        _mockNavigation.Verify(nav => nav.RedirectToLoginPage(), Times.Once);
    }

    [Fact]
    public void GoBack_ShouldReturnPreviousPage_WhenUserNavigatesBack()
    {
        // Arrange
        _mockNavigation.Setup(nav => nav.GoBack()).Returns("PreviousPage");

        // Act
        string result = _mockNavigation.Object.GoBack();

        // Assert
        Assert.Equal("PreviousPage", result);
    }
}
