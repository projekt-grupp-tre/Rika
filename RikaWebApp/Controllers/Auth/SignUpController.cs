using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.AuthModels;
using System.Text;

namespace RikaWebApp.Controllers.Auth
{
    public class SignUpController : Controller
    {
        private readonly HttpClient _httpClient;

        public SignUpController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult SignUpView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                using HttpClient http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await http.PostAsync("https://rikaregistrationapi-ewdqdmb7ayhwhkaw.westeurope-01.azurewebsites.net/Api/Register", content);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Conflict:
                        TempData["ErrorRegister"] = "Email already exists";
                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        TempData["ErrorRegister"] = "Invalid form data";
                        break;

                    case System.Net.HttpStatusCode.InternalServerError:
                        TempData["ErrorRegister"] = "Internal Server Error";
                        break;

                    case System.Net.HttpStatusCode.Created:
                        return RedirectToAction("Index", "Home");
                }

                return View("SignUpView", model);
            }

            return View("SignUpView", model);
        }
    }
}
