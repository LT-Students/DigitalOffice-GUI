using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using System.Threading.Tasks;
using System;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserName();

        Task<string> CreateUser(CreateUserRequest request);

        Task<string> GeneratePassword();

        Task<UsersResponse> GetUsers(Guid departmentId, int skipCount, int takeCount);
    }
}
