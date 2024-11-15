using Business.Dto.AuthDtos;
using RikaWebApp.Models.AuthModels;
using System.Net.NetworkInformation;

namespace RikaWebApp.Helpers.AuthHelpers
{
    public static class TokenConvertionFactory
    {
        public static TokenDto TokenModelConvert(TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                throw new ArgumentNullException("model");
            }

            return new TokenDto
            {
                AccessToken = tokenModel.AccessToken,
                ExpiresAt = tokenModel.ExpiresAt,
                RefreshToken = tokenModel.RefreshToken,
                UserId = tokenModel.UserId,
            };
        }
    }
}
