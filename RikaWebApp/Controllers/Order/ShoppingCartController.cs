using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Order
{
    public class ShoppingCartController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
