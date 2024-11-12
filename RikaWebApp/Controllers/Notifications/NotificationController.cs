using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Notifications
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
