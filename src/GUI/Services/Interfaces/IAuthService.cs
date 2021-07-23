using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(AuthenticationRequest request);

        Task RefreshTokenAsync();

        bool Logout();
    }
}
