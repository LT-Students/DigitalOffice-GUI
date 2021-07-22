using Blazored.SessionStorage;
using GUI.Pages.Auth;
using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthStateProvider _provider;
        private readonly ISessionStorageService _storage;
        private readonly AuthServiceClient _client;

        public AuthService(
            AuthenticationStateProvider provider,
            ISessionStorageService storage)
        {
            _provider = provider as AuthStateProvider;
            _storage = storage;
            _client = new AuthServiceClient(new HttpClient());
        }

        public async Task LoginAsync(AuthenticationRequest request)
        {
            var response = await _client.LoginAsync(request);

            _provider.LoginNotify(response.UserId);

            await _storage.SetItemAsync(nameof(Consts.AccessToken), response.AccessToken);
            await _storage.SetItemAsync(nameof(Consts.RefreshToken), response.RefreshToken);
            await _storage.SetItemAsync(nameof(Consts.AccessTokenExpiresIn), response.AccessTokenExpiresIn);
            await _storage.SetItemAsync(nameof(Consts.RefreshTokenExpiresIn), response.RefreshTokenExpiresIn);
            await _storage.SetItemAsync(nameof(Consts.RefreshToken), response.RefreshToken);
            await _storage.SetItemAsync(nameof(Consts.UserId), response.UserId);
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
