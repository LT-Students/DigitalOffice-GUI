using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Pages.Public
{
    public partial class CreateCredentials
    {
        [Parameter]
        public Guid Id { get; set; }

        private CreateCredentialsRequest _credentialsData = new();
        private string _message;

        private ElementReference _passwordInput;

        protected async override void OnInitialized()
        {
            _credentialsData.UserId = Id;
            StateHasChanged();
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                await userService.CreateCredentials(_credentialsData);
                UriHelper.NavigateTo("");
            }
            catch (ApiException<ErrorResponse> ex)
            {
                _message = ex.Result.Message;
            }
            catch (Exception ex)
            {
                //remove when spec changed
                _message = ex.Message;
            }
        }
    }
}
