﻿using Blazored.SessionStorage;
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
        private readonly ISessionStorageService _storage;

        public UserService(AuthenticationStateProvider provider, ISessionStorageService storage)
        {
            _provider = provider as AuthStateProvider;
            _storage = storage;
        }

        public async Task<string> Login(AuthenticationRequest request)
        {
            try
            {
                var authService = new AuthServiceClient(new System.Net.Http.HttpClient());
                var response = await authService.LoginAsync(request);

                _provider.LoginNotify(response);
                await _storage.SetItemAsync("Token", response.Token);

                return "Authorized";
            }
            catch (ApiException<ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
            catch (ApiException exc)
            {
                return string.Empty;
            }
            catch (Exception exc)
            {
                return string.Empty;
            }
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
