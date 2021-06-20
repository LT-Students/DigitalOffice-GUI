using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserNameAsync();

        Task CreateUserAsync(CreateUserRequest request);

        Task CreateCredentialsAsync(CreateCredentialsRequest request);

        Task<string> GeneratePasswordAsync();
    }
}
