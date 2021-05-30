using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Services.Client.UserService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class UserService : IUserService
    {
        private readonly ISessionStorageService _sessionStorage;

        public UserService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<string> GetUserName()
        {
            if (await _sessionStorage.ContainKeyAsync("UserName"))
            {
                return await _sessionStorage.GetItemAsync<string>("UserName");
            }

            try
            {
                var userServiceClient = new UserServiceClient(new System.Net.Http.HttpClient());

                var token = await _sessionStorage.GetItemAsync<string>("Token");
                var userId = await _sessionStorage.GetItemAsync<Guid>("UserId");

                var userInfo = await userServiceClient.GetUserAsync(token, userId, null, null, null, null, null, null, null, null, null, null, null);

                var userName = $"{userInfo.User.LastName} {userInfo.User.FirstName}";

                await _sessionStorage.SetItemAsync("UserName", userName);

                return userName;
            }
            catch (ApiException<ErrorResponse> exc)
            {
                // TODO: implement catching

                return string.Empty;
            }
        }
    }
}
