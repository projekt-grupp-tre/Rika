using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Helpers;


namespace RikaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = GetCookieInfoHelper.JwtTokenToBasicLoggedInUserModel(HttpContext);

            return View(user);
        }
    }
}
