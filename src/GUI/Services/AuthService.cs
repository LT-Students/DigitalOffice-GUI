using Blazored.SessionStorage;
using GUI.Pages.Auth;
using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;
using LT.DigitalOffice.GUI.Services.Interfaces;
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

        public AuthService(AuthenticationStateProvider provider, ISessionStorageService storage)
        {
            _provider = provider as AuthStateProvider;
            _storage = storage;
        }

        public async Task<string> Login(AuthenticationRequest request)
        {
            try
            {
                var httpClient = new HttpClient();

                var authService = new AuthServiceClient(httpClient);
                var response = await authService.LoginAsync(request);

                _provider.LoginNotify(response);

                await _storage.SetItemAsync(nameof(AuthenticationResponse.Token), response.Token);
                await _storage.SetItemAsync(nameof(AuthenticationResponse.UserId), response.UserId);

                return "Authorized";
            }
            catch (ApiException<ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
