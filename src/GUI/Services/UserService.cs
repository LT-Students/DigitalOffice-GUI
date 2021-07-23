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
        private readonly RefreshTokenHelper _refreshToken;
        private readonly UserServiceClient _userServiceClient;
        private readonly ISessionStorageService _sessionStorage;
        private readonly UserServiceClient _client;

        public UserService(ISessionStorageService sessionStorage, IAuthService authService)
        {
            _sessionStorage = sessionStorage;
            _refreshToken = new(authService, sessionStorage);
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
                await _refreshToken.RefreshAsync();
                var token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);
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

        public async Task<UsersResponse> GetUsers( int skipCount, int takeCount, Guid? departmentId = default)
        {
            try
            {
                await _refreshToken.RefreshAsync();
                string token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);
                var usersResponse = await _userServiceClient.FindUsersAsync(token, departmentId, skipCount, takeCount);

                return usersResponse;
            }
            catch (ApiException<ErrorResponse> exc)
            {
                return null;
            }
        }

        public async Task<string> CreateUser(CreateUserRequest request)
        {
            try
            {
                await _refreshToken.RefreshAsync();
                var token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);
                var response = await _client.CreateUserAsync(request, token);

                return response.Status.ToString();
            }
            catch (ApiException<ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
            catch (Exception ex)
            {
                //remove when spec reworked
                return ex.Message;
            }
        }

        public async Task<string> GeneratePassword()
        {
            try
            {
                await _refreshToken.RefreshAsync();
                var token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);
                return await _client.GeneratePasswordAsync(token);
            }
            catch(ApiException<ErrorResponse> ex)
            {
                return String.Empty;
            }
        }
    }
}
