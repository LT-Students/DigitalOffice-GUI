using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserName();

        Task CreateUser(CreateUserRequest request);

        Task CreateCredentials(CreateCredentialsRequest request);

        Task<string> GeneratePassword();
    }
}
