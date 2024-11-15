using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.AuthModels;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using Business.Services.AuthServices;
using RikaWebApp.Helpers.AuthHelpers;

namespace RikaWebApp.Controllers.Auth
{
    public class SignInController : Controller
    {
        private readonly TokenManagerService _tokenManagerService;
        private readonly IConfiguration _configuration;

        public SignInController(TokenManagerService tokenManagerService, IConfiguration configuration)
        {
            _tokenManagerService = tokenManagerService;
            _configuration = configuration;
        }

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

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(_configuration["Values:ApiKey"]! ?? "");

                var result = await client.PostAsJsonAsync("https://localhost:7286/api/SignIn", signInModel);

                switch (result.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        var userInfo = await result.Content.ReadAsStringAsync();
                        var tokenModel = JsonConvert.DeserializeObject<TokenModel>(userInfo);
                        _tokenManagerService.SetTokens(TokenConvertionFactory.TokenModelConvert(tokenModel!));
                        
                        //HttpContext.Session.SetString("JwtToken", jwtTokenString!);

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
