using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Auth
{
    public class SignOutController : Controller
    {
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("JwtToken");
            TempData["LogOut"] = "You have successfully signed ut! Welcome back another time!";
            return RedirectToAction("Index", "Home");
        }
    }
}
