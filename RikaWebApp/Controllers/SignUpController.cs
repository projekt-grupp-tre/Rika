using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models;
using System.Text;

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
            if(ModelState.IsValid)
            {
                using HttpClient http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await http.PostAsync("https://localhost:7286/api/Register", content);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Conflict:
                        TempData["ErrorRegister"] = "Email already exists";
                        return View("SignUpView", model);

                    case System.Net.HttpStatusCode.BadRequest:
                        TempData["ErrorRegister"] = "Invalid form data";
                        return View("SignUpView", model);

                    case System.Net.HttpStatusCode.InternalServerError:
                        TempData["ErrorRegister"] = "Internal Server Error";
                        return View("SignUpView", model);

                    case System.Net.HttpStatusCode.Created:
                        return RedirectToAction("Index", "Home");
                }
            }

            return View("SignUpView", model);
        }
    }
}
