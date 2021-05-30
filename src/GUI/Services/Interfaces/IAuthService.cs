using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.Client.AuthService;

namespace LT.DigitalOffice.GUI.Services
{
    public interface IAuthService
    {
        Task<string> Login(AuthenticationRequest request);

        bool Logout();
    }
}
