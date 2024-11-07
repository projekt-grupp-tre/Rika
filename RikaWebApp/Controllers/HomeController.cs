using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Helpers;
using RikaWebApp.Models.AuthModels;
using System.Collections;


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
            return View();
        }
    }
}
