using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.Client.AuthService;

namespace LT.DigitalOffice.GUI.Services
{
    public interface IUserService
    {
        Task<string> Login(AuthenticationRequest request);

        bool Logout();
    }
}
