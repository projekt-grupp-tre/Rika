using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Product
{
    public class CategorySelectionController : Controller
    {
        [Route("categories")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
