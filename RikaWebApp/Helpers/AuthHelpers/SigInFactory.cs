using Business.Dto.AuthDtos;
using RikaWebApp.Models.AuthModels;
using System.Net.NetworkInformation;

namespace RikaWebApp.Helpers.AuthHelpers
{
    public static class SigInFactory
    {
        public static SignInDto SignInConvert(SignInModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return new SignInDto
            {
                Email = model.Email,
                Password = model.Password,
            };
        }
    }
}
