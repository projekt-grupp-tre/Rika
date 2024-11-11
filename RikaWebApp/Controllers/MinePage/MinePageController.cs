using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.MinePage
{
    public class MinePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
