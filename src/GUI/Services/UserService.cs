﻿using System;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using LT.DigitalOffice.GUI.Services.Interfaces;

namespace LT.DigitalOffice.GUI.Services
{
  public class UserService : IUserService
  {
    //"http://localhost:9802";/*"https://user.dev.ltdo.xyz";*/
    private readonly RefreshTokenHelper _refreshToken;
    private readonly ISessionStorageService _sessionStorage;
    private readonly UserServiceClient _client;
    private string _token;

    public UserService(ISessionStorageService sessionStorage, IAuthService authService)
    {
      _sessionStorage = sessionStorage;
      _refreshToken = new(authService, sessionStorage);
      _client = new UserServiceClient(new System.Net.Http.HttpClient());
    }

    public async Task<bool> IsAdminAsync()
    {
      return await _sessionStorage.ContainKeyAsync(Consts.IsAdmin)
        ? await _sessionStorage.GetItemAsync<bool>(Consts.IsAdmin)
        : false;
    }

    public async Task<string> GetUserNameAsync()
    {
      if (await _sessionStorage.ContainKeyAsync(Consts.UserName))
      {
        return await _sessionStorage.GetItemAsync<string>(Consts.UserName);
      }

      Guid userId = Guid.Empty;

      while (_token == null || userId == Guid.Empty)
      {
        _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);
        userId = await _sessionStorage.GetItemAsync<Guid>(Consts.UserId);
      }

      var userInfo = await _client.GetUserAsync(
        _token,
        userId,
        null,
        null,
        true,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null);

      var userName = $"{userInfo.Body.User.LastName} {userInfo.Body.User.FirstName}";

      await _sessionStorage.SetItemAsync(Consts.UserName, userName);
      await _sessionStorage.SetItemAsync(Consts.IsAdmin, userInfo.Body.User.IsAdmin);

      return userName;
    }

    public async Task<OperationResultResponseUserResponse> GetAuthorizedUserAsync(bool includeCommunications = true, bool includeProjects = false)
    {
      await _refreshToken.RefreshAsync();
      var userId = await _sessionStorage.GetItemAsync<Guid>(Consts.UserId);
      _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.GetUserAsync(
        _token,
        userId,
        null,
        null,
        includeCommunications,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        includeProjects,
        null,
        null,
                null);
    }

    public async Task<FindResultResponseUserInfo> FindUsersAsync(int skipCount, int takeCount, Guid? departmentId = default)
    {
      await _refreshToken.RefreshAsync();

      _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.FindUsersAsync(_token, skipCount, takeCount, null, null, null, null, null);
    }

    public async Task<OperationResultResponse> CreateUserAsync(CreateUserRequest request)
    {
      await _refreshToken.RefreshAsync();

      _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.CreateUserAsync(request, _token);
    }

    public async Task<string> GeneratePasswordAsync()
    {
      await _refreshToken.RefreshAsync();

      _token = await _sessionStorage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.GeneratePasswordAsync(_token);
    }

    public async Task<OperationResultResponseCredentialsResponse> CreateCredentialsAsync(CreateCredentialsRequest request)
    {
      await _refreshToken.RefreshAsync();

      return await _client.CreateCredentialsAsync(request);
    }
  }
}
