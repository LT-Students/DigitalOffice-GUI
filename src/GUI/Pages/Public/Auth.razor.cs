using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Pages.Public
{
    public partial class Auth
    {
        private AuthenticationRequest _authData = new();
        private string _message;

        private ElementReference _passwordInput;

        private async Task HandleValidSubmit()
        {
            try
            {
                await authService.Login(_authData);
                UriHelper.NavigateTo("");
            }
            catch (ApiException<ErrorResponse> ex)
            {
                _message = ex.Result.Message;
            }
        }
    }
}
