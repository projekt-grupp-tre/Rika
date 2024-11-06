using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Auth;

public class SuccessController : Controller
{
    public IActionResult SuccessView()
    {
        return View();
    }
}
