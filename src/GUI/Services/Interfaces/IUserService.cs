using LT.DigitalOffice.GUI.Services.Client.UserService;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserName();

        public Task<string> CreateUser(CreateUserRequest request);
    }

}
