﻿using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Auth
{
  public partial class CreateCredentials
  {
    [Parameter]
    public Guid Id { get; set; }

    private CreateCredentialsRequest _credentialsData = new();
    private string _message;

    private ElementReference _passwordInput;

    protected override void OnInitialized()
    {
      _credentialsData.UserId = Id;
      StateHasChanged();
    }

    private async Task HandleValidSubmitAsync()
    {
      try
      {
        var loginResponse = await userService.CreateCredentialsAsync(_credentialsData);

        await authService.SetLoginStateAsync(
          loginResponse.Body.UserId,
          loginResponse.Body.AccessToken,
          loginResponse.Body.RefreshToken);

        UriHelper.NavigateTo("");
      }
      catch (ApiException<OperationResultResponse> ex)
      {
        _message = String.Join(" ", ex.Result.Errors);
      }
      catch (ApiException ex)
      {
        _message = "Something went wrong";
      }
    }
  }
}
