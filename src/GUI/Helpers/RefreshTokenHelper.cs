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
            int unixTimestamp = (int)(DateTime.UtcNow.Subtract(DateTime.UnixEpoch)).TotalSeconds;
            
            if (unixTimestamp >= int.Parse((await _storage.GetItemAsync<string>(Consts.AccessTokenExpiresIn))) 
                || unixTimestamp >= int.Parse((await _storage.GetItemAsync<string>(Consts.RefreshTokenExpiresIn))))
            {
                await _authService.RefreshTokenAsync();
            }
        }
    }
}