using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserName();

        public Task<string> CreateUser(CreateUserRequest request);
    }

}
