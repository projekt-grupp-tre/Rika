namespace Business.Interfaces.OrderInterfaces;

public interface INavigation
{
    string GoToNextPage();
    string GoTo404Page();
    string RedirectToLoginPage();
    string GoBack();
}
