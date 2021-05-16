using GUI.Pages.Auth;
using LT.DigitalOffice.AuthService;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class UserService : IUserService
    {
        private readonly AuthStateProvider _provider;

        public UserService(AuthenticationStateProvider provider)
        {
            _provider = provider as AuthStateProvider;
        }

        public async Task<bool> LoginAsync(AuthenticationRequest request)
        {
            var authService = new AuthService.AuthService(new System.Net.Http.HttpClient());
            var response = await authService.LoginAsync(request);

            _provider.LoginNotify(response);

            return true;
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
