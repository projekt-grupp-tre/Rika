using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.AuthModels;
using System.Reflection;

namespace RikaWebApp.Controllers.Auth
{
    public class SignInController : Controller
    {
        [HttpGet]
        public IActionResult SignInView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(SignInModel signInModel)
        {
            if(ModelState.IsValid)
            {
                using HttpClient client = new HttpClient();
                var result = await client.PostAsJsonAsync("https://rikaregistrationapi-ewdqdmb7ayhwhkaw.westeurope-01.azurewebsites.net/Api/SignIn", signInModel);
                
                switch (result.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var userInfo = await result.Content.ReadAsStringAsync();
                        var basicUserInfo = JsonConvert.DeserializeObject<BasicLoggedInUser>(userInfo);
                        return RedirectToAction("Index", "Home", basicUserInfo);
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
                        TempData["ErrorLogin"] = "Something wnet wrong. Please try again later";
                        break;
                }
            }
            return View("SignInView", signInModel);
        }
    }
}
