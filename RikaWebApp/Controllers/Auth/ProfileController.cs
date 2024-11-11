using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Auth;

public class ProfileController : Controller
{
    public IActionResult ProfileView()
    {
        return View();
    }
}
