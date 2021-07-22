using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using System.Threading.Tasks;
using System;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserNameAsync();

        Task CreateUserAsync(CreateUserRequest request);

        Task CreateCredentialsAsync(CreateCredentialsRequest request);

        Task<string> GeneratePassword();

        Task<UsersResponse> GetUsers(int skipCount, int takeCount, Guid? departmentId);
    }
}
