using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserName();

        Task<string> CreateUser(CreateUserRequest request);
    }
}
