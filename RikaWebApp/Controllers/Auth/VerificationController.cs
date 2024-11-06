using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Auth
{
    public class VerificationController : Controller
    {
        public IActionResult VerificationView()
        {
            return View();
        }
    }
}

