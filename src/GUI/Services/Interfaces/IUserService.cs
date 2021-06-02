using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserName();

        Task<UsersResponse> GetUsers();
    }
}
