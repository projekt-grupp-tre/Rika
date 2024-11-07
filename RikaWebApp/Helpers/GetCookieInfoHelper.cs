using Azure.Core;
using RikaWebApp.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Models.AuthModels;

namespace RikaWebApp.Helpers;

public static class GetCookieInfoHelper
{
    public static BasicLoggedInUser JwtTokenToBasicLoggedInUserModel(HttpContext context)
    {
        if (context.Request.Cookies["JwtToken"] != null)
        {
            string token = context.Request.Cookies["JwtToken"]!;
            var claims = JwtHelper.ParseJwt(token);
            
            return new BasicLoggedInUser
            {
                Id = claims.ContainsKey("sub") ? claims["sub"]?.ToString() ?? "" : "",
                FristName = claims.ContainsKey("firstName") ? claims["firstName"]?.ToString() ?? "" : "",
                LastName = claims.ContainsKey("lastName") ? claims["lastName"]?.ToString() ?? "" : "",
                Email = claims.ContainsKey("email") ? claims["email"]?.ToString() ?? "" : "",
                ImageUrl = claims.ContainsKey("imageUrl") ? claims["imageUrl"]?.ToString() ?? "" : ""
            };
        }

        return null!;
    }
}
