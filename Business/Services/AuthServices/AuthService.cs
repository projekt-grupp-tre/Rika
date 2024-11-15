using Azure;
using Business.Dto.AuthDtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace Business.Services.AuthServices;

public class AuthService
{  
    public async Task<TokenDto> RefreshToken(string refreshToken, string userId)
    {
        var refreshRequest = new
        {
            RefreshToken = refreshToken,
            UserId = userId
        };

        using HttpClient _httpClient = new HttpClient();
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7286/api/Authorization/refresh", refreshRequest);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TokenDto>();
        }
        throw new Exception("Token refresh failed");
    }
}
