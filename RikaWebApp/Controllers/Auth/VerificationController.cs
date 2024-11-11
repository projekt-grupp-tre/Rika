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
            var response = await http.PostAsync("https://verification-rika.azurewebsites.net/api/verify?code=0mQbf3eCNmaWk7YjezTtO2DZNsI0gDN6QD9p7Cd10z11AzFuY415IA%3D%3D", content);
            
            if (response.IsSuccessStatusCode)
            {
                var emailContent = new StringContent(JsonConvert.SerializeObject(new { Email = email }), Encoding.UTF8, "application/json");
                var emailConfirmedResponse = await http.PostAsync("https://rikaregistrationapi-ewdqdmb7ayhwhkaw.westeurope-01.azurewebsites.net/Api/Verification", emailContent);

                if (emailConfirmedResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("SuccessView", "Success");
                }
            }
        }

        return View();
    }
}

