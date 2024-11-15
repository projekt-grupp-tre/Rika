using Business.Services.AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Auth
{
    public class SignOutController : Controller
    {
        private readonly TokenManagerService _tokenManager;

        public SignOutController(TokenManagerService tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public IActionResult LogOut()
        {
            _tokenManager.ClearTokens();
            TempData["LogOut"] = "You have successfully signed ut! Welcome back another time!";
            return RedirectToAction("Index", "Home");
        }
    }
}
