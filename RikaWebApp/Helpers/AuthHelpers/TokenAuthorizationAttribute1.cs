using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Business.Services.AuthServices;

namespace RikaWebApp.Helpers.AuthHelpers
{
    public class TokenAuthorizationOnActionAttribute : ActionFilterAttribute
    {
        //Denna del gör så att du kan sätta ett Authorizes attribut på en enskild action i en controller. Den kollar så att man har en Accesstoken. 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tokenManager = context.HttpContext.RequestServices.GetService<TokenManagerService>();
            var accessToken = tokenManager?.GetAccessToken();

            if (string.IsNullOrEmpty(accessToken))
            {
                context.Result = new RedirectToActionResult("LoginAsync", "SignIn", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
