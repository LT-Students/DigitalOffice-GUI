using System;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Services.Interfaces;

namespace LT.DigitalOffice.GUI.Helpers
{
    public class RefreshTokenHelper
    {
        private readonly IAuthService _authService;
        private readonly ISessionStorageService _storage;

        public RefreshTokenHelper(IAuthService authService, ISessionStorageService storage)
        {
            _storage = storage;
            _authService = authService;
        }

        public async Task RefreshAsync()
        {
            int accessTokenExpiresIn = int.Parse((await _storage.GetItemAsync<string>(Consts.AccessTokenExpiresIn)));
            if (DateTime.UnixEpoch.Second >=  accessTokenExpiresIn)
            {
                await _authService.RefreshTokenAsync();
            }
        }
    }
}