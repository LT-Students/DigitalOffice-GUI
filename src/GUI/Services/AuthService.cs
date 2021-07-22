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

        public async Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request)
        {
            return await _client.LoginAsync(request);
        }

        public async Task LoginStateAsync(
            Guid userId,
            string accessToken,
            string refreshToken,
            double accessTokenExpiresIn,
            double refreshTokenExpiresIn)
        {
            _provider.LoginNotify(userId);

            await _storage.SetItemAsync(nameof(Consts.AccessToken), accessToken);
            await _storage.SetItemAsync(nameof(Consts.RefreshToken), refreshToken);
            await _storage.SetItemAsync(nameof(Consts.AccessTokenExpiresIn), accessTokenExpiresIn);
            await _storage.SetItemAsync(nameof(Consts.RefreshTokenExpiresIn), refreshTokenExpiresIn);
            await _storage.SetItemAsync(nameof(Consts.UserId), userId);
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
