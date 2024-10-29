using Microsoft.AspNetCore.Mvc;


namespace RikaWebApp.Controllers
{
    public class ContactController(ILogger<ContactController> logger) : Controller
    {
        private readonly ILogger<ContactController> _logger = logger;

				[HttpGet]
				[Route("/contact")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
