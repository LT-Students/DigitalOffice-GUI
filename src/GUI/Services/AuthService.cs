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

        private async Task SetTokenValues(AuthenticationResponse response)
        {
            var handler = new JwtSecurityTokenHandler();

            Func<string, string> tokenExpiresIn = token => ((handler.ReadToken(token)) as JwtSecurityToken)
                .Claims.First(x => string.Equals("exp", x.Type)).Value;
            
            await _storage.SetItemAsync(nameof(Consts.AccessToken), response.AccessToken);
            await _storage.SetItemAsync(nameof(Consts.RefreshToken), response.RefreshToken);
            await _storage.SetItemAsync(nameof(Consts.AccessTokenExpiresIn), tokenExpiresIn(response.AccessToken));
            await _storage.SetItemAsync(nameof(Consts.RefreshTokenExpiresIn), tokenExpiresIn(response.RefreshToken));
            await _storage.SetItemAsync(nameof(Consts.RefreshToken), response.RefreshToken);
        }

        public AuthService(AuthenticationStateProvider provider, ISessionStorageService storage)
        {
            _authService = new AuthServiceClient(new HttpClient());
            _provider = provider as AuthStateProvider;
            _storage = storage;
        }

        public async Task<string> Login(AuthenticationRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);

                _provider.LoginNotify(response);

                await SetTokenValues(response);
                await _storage.SetItemAsync(nameof(Consts.UserId), response.UserId);

                return "Authorized";
            }
            catch (ApiException<ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
        }

        public async Task RefreshTokenAsync()
        {
            RefreshRequest body = new() 
            {
                RefreshToken = await _storage.GetItemAsync<string>(Consts.RefreshToken)
            };

            await SetTokenValues(await _authService.RefreshAsync(body));
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
