namespace RikaWebApp.Middleware
{
    public class JwtSlidingExpirationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtSlidingExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("JwtToken", out var token))
            {
                if (!string.IsNullOrEmpty(token))
                {       
                    DateTimeOffset newExpiration = DateTimeOffset.UtcNow.AddMinutes(60);

                    context.Response.Cookies.Append("JwtToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = newExpiration
                    });
                }
            }
            await _next(context);
        }
    }
}
