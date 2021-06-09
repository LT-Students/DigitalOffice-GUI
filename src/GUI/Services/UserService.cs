using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class UserService : IUserService
    {
        private readonly UserServiceClient _userServiceClient;
        private readonly ISessionStorageService _sessionStorage;

        public UserService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
            _userServiceClient = new UserServiceClient(new System.Net.Http.HttpClient());
        }

        public async Task<string> GetUserName()
        {
            if (await _sessionStorage.ContainKeyAsync(Consts.UserName))
            {
                return await _sessionStorage.GetItemAsync<string>(Consts.UserName);
            }

            try
            {
                var token = await _sessionStorage.GetItemAsync<string>(Consts.Token);
                var userId = await _sessionStorage.GetItemAsync<Guid>(Consts.UserId);

                var userInfo = await _userServiceClient.GetUserAsync(token, userId, null, null, null, null, null, null, null, null, null, null, null);

                var userName = $"{userInfo.User.LastName} {userInfo.User.FirstName}";

                await _sessionStorage.SetItemAsync(Consts.UserName, userName);

                return userName;
            }
            catch (ApiException<ErrorResponse> exc)
            {
                // TODO: implement catching

                return string.Empty;
            }
        }

        public async Task<UsersResponse> GetUsers(Guid departmentId, int skipCount, int takeCount)
        {
            try
            {
                string token = await _sessionStorage.GetItemAsStringAsync(Consts.Token);

                var usersResponse = await _userServiceClient.FindUsersAsync(token, skipCount, takeCount);

                return usersResponse;
            }
            catch (ApiException<ErrorResponse> exc)
            {
                return null;
            }
        }
    }
}
