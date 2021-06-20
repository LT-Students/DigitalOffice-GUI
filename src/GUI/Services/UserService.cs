using Blazored.SessionStorage;
using GUI.Pages.Auth;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class UserService : IUserService
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly UserServiceClient _client;
        private readonly AuthStateProvider _provider;

        public UserService(
            ISessionStorageService sessionStorage,
            AuthenticationStateProvider provider)
        {
            _sessionStorage = sessionStorage;
            _client = new UserServiceClient(new System.Net.Http.HttpClient());
            _provider = provider as AuthStateProvider;
        }

        public async Task<string> GetUserNameAsync()
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

        public async Task CreateUserAsync(CreateUserRequest request)
        {
            var token = await _sessionStorage.GetItemAsync<string>(Consts.Token);

            await _client.CreateUserAsync(request, token);
        }

        public async Task CreateCredentialsAsync(CreateCredentialsRequest request)
        {
            var response = await _client.CreateCredentialsAsync(request);

            _provider.LoginNotify(response.UserId);

            await _sessionStorage.SetItemAsync(nameof(CredentialsResponse.Token), response.Token);
            await _sessionStorage.SetItemAsync(nameof(CredentialsResponse.UserId), response.UserId);
        }

        public async Task<string> GeneratePasswordAsync()
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
