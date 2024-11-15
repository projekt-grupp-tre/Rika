using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Helpers.AuthHelpers;

namespace RikaWebApp.Controllers.Auth;

public class ProfileController : Controller
{
    public IActionResult ProfileView()
    {
        return View();
    }
}
