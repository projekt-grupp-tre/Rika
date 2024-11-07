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
                var result = await client.PostAsJsonAsync("https://localhost:7286/api/SignIn", signInModel);
                
                switch (result.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var userInfo = await result.Content.ReadAsStringAsync();
                        var basicUserInfo = JsonConvert.DeserializeObject<BasicLoggedInUser>(userInfo);
                        //HttpContext.Session.SetString("JwtToken", basicUserInfo!.Token);

                        Response.Cookies.Append("JwtToken", basicUserInfo!.jwttoken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddHours(1)
                        });

                        string token = Request.Cookies["JwtToken"].ToString();

                        if (!string.IsNullOrEmpty(token))
                        {
                            var claims = JwtHelper.ParseJwt(token);

                            Console.WriteLine(claims["firstName"]); 
                        }
                        else
                        {
                            Console.WriteLine("Ingen JWT-token hittades.");
                        }

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
                        TempData["ErrorLogin"] = "Something wnet wrong. Please try again later";
                        break;
                }
            }
            return View("SignInView", signInModel);
        }
    }
}
