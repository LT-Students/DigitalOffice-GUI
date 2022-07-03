using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using Microsoft.AspNetCore.Components;
using OperationResultResponse = LT.DigitalOffice.GUI.Services.ApiClients.UserService.OperationResultResponse;

namespace LT.DigitalOffice.GUI.Pages.Admin.User
{
  public partial class CreateUser
  {
    private CreateUserRequest _userData = new();
    private CreateCommunicationRequest _userCommunication = new();
    private string _message;
    private bool _isSuccessOperation;

    private ElementReference _lastNameInput;
    private ElementReference _middleNameInput;
    private ElementReference _emailInput;
    private ElementReference _passwordInput;

    public CreateUser()
    {
      _userCommunication.Type = CommunicationType.Email;
      _userData.Communication = _userCommunication;
    }

    private async Task GeneratePasswordAsync()
    {
      _userData.Password = await userService.GeneratePasswordAsync();
    }

    private async Task HandleValidSubmitAsync()
    {
      try
      {
        await userService.CreateUserAsync(_userData);
        _message = "Successfully created";
        _isSuccessOperation = true;
      }
      catch (ApiException<OperationResultResponse> ex)
      {
        _message = String.Join(" ", ex.Result.Errors);

      }
      catch (ApiException ex)
      {
        _message = "Something went wrong";
      }

      StateHasChanged();
    }
  }
}
