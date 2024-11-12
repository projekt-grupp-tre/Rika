using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.AuthModels;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;

namespace RikaWebApp.Controllers.Auth
{
    public class SignInController : Controller
    {
        [HttpGet]
        public IActionResult SignInView()
        {
            if(Request.Cookies["JwtToken"] == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(SignInModel signInModel)
        {
            
            if(ModelState.IsValid)
            {
                using HttpClient client = new HttpClient();

                var result = await client.PostAsJsonAsync("https://rikaregistrationapi-ewdqdmb7ayhwhkaw.westeurope-01.azurewebsites.net/api/SignIn", signInModel);

                switch (result.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var userInfo = await result.Content.ReadAsStringAsync();
                        var jwtTokenString = JsonConvert.DeserializeObject<JwtTokenStringModel>(userInfo);

                        Response.Cookies.Append("JwtToken", jwtTokenString!.jwttoken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                        });
                        return RedirectToAction("Index", "Home");

                    case System.Net.HttpStatusCode.Unauthorized:
                        TempData["ErrorLogin"] = "Wrong email or password";
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        TempData["ErrorLogin"] = "Invalid Email or password format";
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        TempData["ErrorLogin"] = "Internal Server Error";
                        break;
                    default:
                        TempData["ErrorLogin"] = "Something went wrong. Please try again later";
                        break;
                }
            }
            return View("SignInView", signInModel);
        }
    }
}
