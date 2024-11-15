using Business.Services.AuthServices;
using System.Net.Http.Headers;
using System.Net;

namespace RikaWebApp.Helpers.AuthHelpers
{
    // Handlers/HttpClientAuthorizationHandler.cs
    public class HttpClientAuthorizationHandler : DelegatingHandler
    {
        private readonly TokenManagerService _tokenManager;
        private readonly AuthService _authService;

        public HttpClientAuthorizationHandler(TokenManagerService tokenManager, AuthService authService)
        {
            _tokenManager = tokenManager;
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = _tokenManager.GetAccessToken();
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshToken = _tokenManager.GetRefreshToken();
                var userId = _tokenManager.GetUserId();

                if (!string.IsNullOrEmpty(refreshToken) && !string.IsNullOrEmpty(userId))
                {
                    try
                    {
                        var newTokens = await _authService.RefreshToken(refreshToken, userId);
                        _tokenManager.SetTokens(newTokens);

                        // Upprepa original-requesten med ny token
                        request.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
                        response = await base.SendAsync(request, cancellationToken);
                    }
                    catch
                    {
                        _tokenManager.ClearTokens();
                        throw;
                    }
                }
            }

            return response;
        }
    }
}
