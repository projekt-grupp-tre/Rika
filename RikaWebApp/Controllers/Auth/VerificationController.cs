using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.AuthModels;
using System.Text;

namespace RikaWebApp.Controllers.Auth;

public class VerificationController : Controller
{
    public IActionResult VerificationView()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> VerifyAccount(VerificationModel model)
    {
        if (ModelState.IsValid)
        {
            var email = Request.Cookies["UserEmail"];

            using HttpClient http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new {Email = email, Code = model.VerificationCode}), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("https://verificationprovider-group3.azurewebsites.net/api/verify?code=ye61aXEds-iuSA3YG7OPCbMbLpU5B6-awY7TKB88oBacAzFud3pmkQ%3D%3D", content);
             
        }

        return View();
    }
}

