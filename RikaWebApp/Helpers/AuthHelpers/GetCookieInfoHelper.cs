using RikaWebApp.Models.AuthModels;

namespace RikaWebApp.Helpers;

public static class GetCookieInfoHelper
{
    public static BasicLoggedInUser JwtTokenToBasicLoggedInUserModel(HttpContext context)
    {
        if (context.Request.Cookies["AccessTOken"] != null)
        {
            string token = context.Request.Cookies["AccessToken"]!;
            var claims = JwtHelper.ParseJwt(token);

            return new BasicLoggedInUser
            {
                Id = claims.ContainsKey("sub") ? claims["sub"]?.ToString() ?? "" : "",
                FirstName = claims.ContainsKey("firstName") ? claims["firstName"]?.ToString() ?? "" : "",
                LastName = claims.ContainsKey("lastName") ? claims["lastName"]?.ToString() ?? "" : "",
                Email = claims.ContainsKey("email") ? claims["email"]?.ToString() ?? "" : "",
                ImageUrl = claims.ContainsKey("imageUrl") ? claims["imageUrl"]?.ToString() ?? "" : "",
                Address = claims.ContainsKey("address") ? claims["address"]?.ToString() ?? "" : "",
                PostalCode = claims.ContainsKey("postalCode") ? claims["postalCode"]?.ToString() ?? "" : "",
                Country = claims.ContainsKey("country") ? claims["country"].ToString() ?? "" : "",
                City = claims.ContainsKey("city") ? claims["city"]?.ToString() ?? "" : "",
                Age = claims.ContainsKey("age") ? claims["age"]?.ToString() ?? "" : "",
                Gender = claims.ContainsKey("gender") ? claims["gender"]?.ToString() ?? "" : "",
            };
        }

        return null!;
    }
}
