using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Helpers;
using RikaWebApp.ViewModels;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace RikaWebApp.Controllers.Auth;

public class ProfileMinePageController(HttpClient http, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    [Route("profile/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new ProfileUpdateViewModel();

        try
        {
            var basicUser = GetCookieInfoHelper.JwtTokenToBasicLoggedInUserModel(HttpContext);

            if (basicUser != null)
            {
                viewModel.FirstName = basicUser.FirstName;
                viewModel.LastName = basicUser.LastName;
                viewModel.Email = basicUser.Email;
                viewModel.Address = basicUser.Address;
                viewModel.PostalCode = basicUser.PostalCode;
                viewModel.Country = basicUser.Country;
                viewModel.City = basicUser.City;
                viewModel.ImageUrl = basicUser.ImageUrl;
               // viewModel.Age = (basicUser.Age == "0") ? "" : basicUser.Age; 
            }
            

            return View(viewModel);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return View(viewModel);
    }





    [HttpPost]
    [HttpPost]
    [Route("profile/details")]
    public async Task<IActionResult> Details(ProfileUpdateViewModel detailsViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(detailsViewModel);
        }

        try
        {
            var jwtToken = Request.Cookies["JwtToken"];
            if (jwtToken == null)
            {
                TempData["Error"] = "User is not authenticated";
                return RedirectToAction("SignIn", "SignIn");
            }

            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _http.PostAsJsonAsync("https://localhost:7259/api/Profile/details/Update", detailsViewModel);


            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Profile updated successfully";
                return RedirectToAction("Details");
            }
            else
            {
                TempData["Error"] = "Profile update failed - " + response.ReasonPhrase;
                Debug.WriteLine("ERROR :: Profile update failed - " + response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "An error occurred while updating profile.";
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return View(detailsViewModel);
    }

    [HttpPost]
    [Route("profile/delete")]
    public async Task<IActionResult> DeleteAccount()
    {
        try
        {
            var jwtToken = Request.Cookies["JwtToken"];
            if (jwtToken == null)
            {
                TempData["Error"] = "User is not authenticated";
                return RedirectToAction("SignIn", "SignIn");
            }

            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _http.DeleteAsync("https://localhost:7259/api/Profile/details/Delete");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Account deleted successfully";
                Response.Cookies.Delete("JwtToken");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Failed to delete account - " + response.ReasonPhrase;
                Debug.WriteLine("ERROR :: Account deletion failed - " + response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "An error occurred while deleting the account.";
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return RedirectToAction("Details");
    }


}
