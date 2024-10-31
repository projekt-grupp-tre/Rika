using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Models;

namespace RikaWebApp.Controllers
{
    public class SignUpController : Controller
    {
        [HttpGet]
        public IActionResult SignUpView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpModel model)
        {
            if(!ModelState.IsValid)
            {

            }

            return View("SignUpView", model);
        }
    }
}
