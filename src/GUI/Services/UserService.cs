using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class UserService : IUserService
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly UserServiceClient _client;
        private string _token;

        public UserService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
            _client = new UserServiceClient(new HttpClient());
        }

        public async Task<string> GetUserNameAsync()
        {
            if (await _sessionStorage.ContainKeyAsync(Consts.UserName))
            {
                return await _sessionStorage.GetItemAsync<string>(Consts.UserName);
            }

            _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);
            var userId = await _sessionStorage.GetItemAsync<Guid>(Consts.UserId);

            var userInfo = await _client.GetUserAsync(_token, userId, null, null, null, null, null, null, null, null, null, null, null, null, null);

            var userName = $"{userInfo.User.LastName} {userInfo.User.FirstName}";

            await _sessionStorage.SetItemAsync(Consts.UserName, userName);

            return userName;
        }

        public async Task<UsersResponse> FindUsersAsync( int skipCount, int takeCount, Guid? departmentId = default)
        {
            _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);

            return await _client.FindUsersAsync(_token, departmentId, skipCount, takeCount);
        }

        public async Task<OperationResultResponse> CreateUserAsync(CreateUserRequest request)
        {
            _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);

            return await _client.CreateUserAsync(request, _token);
        }

        public async Task<string> GeneratePasswordAsync()
        {
            _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);

            return await _client.GeneratePasswordAsync(_token);
        }

        public async Task<Response> CreateCredentialsAsync(CreateCredentialsRequest request)
        {
            return await _client.CreateCredentialsAsync(request);
            _client.EditUserAsync
        }
    }
}
