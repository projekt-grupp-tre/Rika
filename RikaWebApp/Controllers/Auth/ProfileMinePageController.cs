using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Helpers;
using RikaWebApp.ViewModels;
using System.Diagnostics;
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
                viewModel.Age = (basicUser.Age == "0") ? "" : basicUser.Age;
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
    public async Task<IActionResult> Details(ProfileUpdateViewModel detailsViewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // en  userId av UsersProvider_Rika
                string userId = "00000000-f85d-429c-96f9-736696f6b190";

                
                var updateModel = new ProfileUpdateViewModel
                {
                    FirstName = detailsViewModel.FirstName,
                    LastName = detailsViewModel.LastName,
                    Email = detailsViewModel.Email,
                    Address = detailsViewModel.Address,
                    City = detailsViewModel.City,
                    PostalCode = detailsViewModel.PostalCode,
                    Country = detailsViewModel.Country,
                    ImageUrl = detailsViewModel.ImageUrl
                };

               
                var json = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");

              
                var requestUrl = $"http://localhost:7074/api/users/{userId}";
                var response = await _http.PostAsync(requestUrl, json);

               
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    Debug.WriteLine("ERROR :: Request failed - " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR :: " + ex.Message);
            }
        }

       
        return View(detailsViewModel);
    }




    // delete

    public async Task<IActionResult> DeleteAccount()
    {
        try
        {

            // en  userId av UsersProvider_Rika
            // string userId = "aaaaaaaa-2dbf-4f6d-aa18-2fff495a9c1c";


            // string userId = "dddddddd-a9e1-4694-9a5d-3c8a4b58fdba";
            // string userId = "eeeeeeee-61b0-4210-bdb5-ccda889c832d";



            //tests
            string userId = "00000000-f85d-429c-96f9-736696f6b190";
            // string userId = "cccccccc-abb5-4421-ad64-3635f31a6233";
            // string userId = "ffffffff-4663-b8e9-e2accb5ce188";


            var requestUrl = $"http://localhost:7074/api/users/{userId}";

          
            var response = await _http.DeleteAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                Debug.WriteLine("ERROR :: Bad request - " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return RedirectToAction("Index");
    }

    
}
