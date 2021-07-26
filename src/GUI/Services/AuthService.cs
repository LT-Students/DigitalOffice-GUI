using Blazored.SessionStorage;
using GUI.Pages.Auth;
using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace LT.DigitalOffice.GUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthServiceClient _authService;
        private readonly AuthStateProvider _provider;
        private readonly ISessionStorageService _storage;

        private async Task SetTokenValues(string accessToken, string refreshToken)
        {
            var handler = new JwtSecurityTokenHandler();

            Func<string, string> tokenExpiresIn = token => ((handler.ReadToken(token)) as JwtSecurityToken)
                .Claims.First(x => string.Equals("exp", x.Type)).Value;

            await _storage.SetItemAsync(nameof(Consts.AccessToken), accessToken);
            await _storage.SetItemAsync(nameof(Consts.RefreshToken), refreshToken);
            await _storage.SetItemAsync(nameof(Consts.AccessTokenExpiresIn), tokenExpiresIn(accessToken));
            await _storage.SetItemAsync(nameof(Consts.RefreshTokenExpiresIn), tokenExpiresIn(refreshToken));
        }

        public AuthService(AuthenticationStateProvider provider, ISessionStorageService storage)
        {
            _authService = new AuthServiceClient(new HttpClient());
            _provider = provider as AuthStateProvider;
            _storage = storage;
        }

        public async Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request)
        {
            return await _authService.LoginAsync(request);
        }

        public async Task SetLoginStateAsync(Guid userId, string accessToken, string refreshToken)
        {
            _provider.LoginNotify(userId);

            await SetTokenValues(accessToken, refreshToken);
            await _storage.SetItemAsync(nameof(Consts.UserId), userId);
        }

        public async Task RefreshTokenAsync()
        {
            RefreshRequest body = new()
            {
                RefreshToken = await _storage.GetItemAsync<string>(Consts.RefreshToken)
            };

            var refreshTokenData = await _authService.RefreshAsync(body);

            await SetTokenValues(refreshTokenData.AccessToken, refreshTokenData.RefreshToken);
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
