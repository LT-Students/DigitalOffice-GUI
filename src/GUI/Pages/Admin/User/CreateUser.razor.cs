using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using OperationResultResponse = LT.DigitalOffice.GUI.Services.ApiClients.UserService.OperationResultResponse;
using PositionInfo = LT.DigitalOffice.GUI.Services.ApiClients.CompanyService.PositionInfo;

namespace LT.DigitalOffice.GUI.Pages.Admin.User
{
    public partial class CreateUser
    {
        private CreateUserRequest _userData = new();
        private CreateCommunicationRequest _userCommunication = new();
        private ICollection<PositionInfo> _positions;
        private string _message;
        private List<float> _rateValues = new List<float> { 0.25f, 0.5f, 0.75f, 1 };

        private ElementReference _lastNameInput;
        private ElementReference _middleNameInput;
        private ElementReference _emailInput;
        private ElementReference _passwordInput;
        private ElementReference _rateSelect;
        private ElementReference _positionSelect;

        public CreateUser()
        {
            _userCommunication.Type = CommunicationType.Email;
            _userData.Communications.Add(_userCommunication);
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
                UriHelper.NavigateTo("/");
            }
            catch(ApiException<ErrorResponse> ex)
            {
                _message = ex.Result.Message;
            }
            catch(ApiException<OperationResultResponse> ex)
            {
                foreach(var error in ex.Result.Errors)
                {
                    _message = _message + " " + error;
                }
            }
            catch (ApiException ex)
            {
                _message = "Something went wrong";
            }
        }

        protected async override void OnInitialized()
        {
            var positionsResponse = await companyService.FindPositionsAsync();
            _positions = positionsResponse.Body;
            StateHasChanged();
        }
    }
}
