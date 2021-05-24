using LT.DigitalOffice.AuthService;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public interface IUserService
    {
        Task<string> Login(AuthenticationRequest request);

        bool Logout();
    }
}
