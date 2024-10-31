using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Ordeer
{
    public class ShoppingCartController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
