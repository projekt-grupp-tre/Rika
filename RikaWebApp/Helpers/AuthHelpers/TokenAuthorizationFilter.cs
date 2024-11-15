using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Business.Services.AuthServices;

namespace RikaWebApp.Helpers.AuthHelpers
{
    // Attributes/TokenAuthorizationAttribute.cs

    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        //Denna del gör så att du kan sätta ett Authorizes attribut på en hel contoller. Den kollar så att man har en Accesstoken. 
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = context.HttpContext.RequestServices.GetService<TokenManagerService>();
            var user = context.HttpContext?.User;
            var isAuth = user?.Identity?.IsAuthenticated;

            if(isAuth == true)
            {
                var accessToken = tokenManager?.GetAccessToken();

                if (string.IsNullOrEmpty(accessToken))
                {
                    context.Result = new RedirectToActionResult("LoginAsync", "SignIn", null);
                    return;
                }
            }
            return;
        }
    }
}
