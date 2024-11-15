using Business.Dto.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Business.Services.AuthServices;

public class TokenManagerService
{
    //IHttpContextAccessor Den används främst för att läsa eller manipulera HTTP-specifika data som är kopplade
    //till den pågående begäran(till exempel begärans headers, cookies, användarens identitet, session, med mera).
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILogger<TokenManagerService> _logger;
    private TokenDto _tokenDto;

    public TokenManagerService(IHttpContextAccessor contextAccessor, ILogger<TokenManagerService> logger)
    {
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    public void SetTokens(TokenDto tokenDto)
    {
        _tokenDto = tokenDto;

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(5)
        };

        _contextAccessor.HttpContext?.Response.Cookies.Append("AccessToken", tokenDto.AccessToken);
        _contextAccessor.HttpContext?.Response.Cookies.Append("RefreshToken", tokenDto.RefreshToken);
        _contextAccessor.HttpContext?.Response.Cookies.Append("UserId", tokenDto.UserId);
    }
    public string? GetAccessToken()
    {
        if(!UserHasPermission())
        {
            _logger.LogWarning("Otillåten åtkomst till GetAccessToken.");
            return null;
        }

        var token = _contextAccessor.HttpContext.Request.Cookies["AccessToken"];
        return IsTokenValid(token) ? token : null;
    }

    public string? GetRefreshToken()
    {
        return _contextAccessor.HttpContext?.Request.Cookies["RefreshToken"];
    }

    public string? GetUserId()
    {
        return _contextAccessor.HttpContext?.Request.Cookies["UserId"];
    }

    public void ClearTokens()
    {
        if(_tokenDto != null!)
        {
            _tokenDto = null!;
        }
        _contextAccessor.HttpContext?.Response.Cookies.Delete("AccessToken");
        _contextAccessor.HttpContext?.Response.Cookies.Delete("RefreshToken");
        _contextAccessor.HttpContext?.Response.Cookies.Delete("UserId");
    }

    private bool IsTokenValid(string? token)
    {
        return !string.IsNullOrEmpty(token) && token.Length > 19;
    }

    private bool UserHasPermission()
    {
        var user = _contextAccessor.HttpContext?.User;
        return user?.Identity?.IsAuthenticated ?? true;
    }
}
