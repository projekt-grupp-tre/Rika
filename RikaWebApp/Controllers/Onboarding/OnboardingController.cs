using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Onboarding
{
    public class OnboardingController : Controller
    {
        [Route("onboarding")]
        public IActionResult Onboarding()
        {
            return View();
        }
    }
}
