using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RikaWebApp.Helpers.AuthHelpers
{
    public class TokenAuthorizationAttribute : TypeFilterAttribute
    {
        public TokenAuthorizationAttribute() : base(typeof(TokenAuthorizationFilter))
        {
        }
    }
}
