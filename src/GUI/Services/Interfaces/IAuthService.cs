using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request);

        Task LoginStateAsync(
            Guid userId,
            string accessToken,
            string refreshToken,
            double accessTokenExpiresIn,
            double refreshTokenExpiresIn);

        bool Logout();
    }
}
