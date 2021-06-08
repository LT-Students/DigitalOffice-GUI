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
        private readonly ISessionStorageService _sessionStorage;
        private readonly UserServiceClient _client;

        public UserService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
            _client = new UserServiceClient(new System.Net.Http.HttpClient());
        }

        public async Task<string> GetUserName()
        {
            if (await _sessionStorage.ContainKeyAsync(Consts.UserName))
            {
                return await _sessionStorage.GetItemAsync<string>(Consts.UserName);
            }

            try
            {
                var userServiceClient = new UserServiceClient(new System.Net.Http.HttpClient());

                var token = await _sessionStorage.GetItemAsync<string>(Consts.Token);
                var userId = await _sessionStorage.GetItemAsync<Guid>(Consts.UserId);

                var userInfo = await userServiceClient.GetUserAsync(token, userId, null, null, null, null, null, null, null, null, null, null, null);

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

        public async Task<string> CreateUser(CreateUserRequest request)
        {
            try
            {
                var token = await _sessionStorage.GetItemAsync<string>(Consts.Token);
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
                var token = await _sessionStorage.GetItemAsync<string>(Consts.Token);
                return await _client.GeneratePasswordAsync(token);
            }
            catch(ApiException<ErrorResponse> ex)
            {
                return String.Empty;
            }
        }
    }
}
