using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.AuthModels;
using System.Diagnostics;
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
                        SetEmailCookie(model.Email);
                        return RedirectToAction("VerificationView", "Verification");
                }

                return View("SignUpView", model);
            }

            return View("SignUpView", model);
        }


        public void SetEmailCookie(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddMinutes(15),
                        SameSite = SameSiteMode.Strict
                    };

                    Response.Cookies.Append("UserEmail", email, cookieOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
