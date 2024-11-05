using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.AuthModels;

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
                if(result.IsSuccessStatusCode)
                {
                    var userInfo = await result.Content.ReadAsStringAsync();
                    var basicUserInfo = JsonConvert.DeserializeObject<BasicLoggedInUser>(userInfo);
                    return RedirectToAction("Index", "Home", basicUserInfo);
                }
            }
            return View("Index", signInModel);
        }
    }
}
