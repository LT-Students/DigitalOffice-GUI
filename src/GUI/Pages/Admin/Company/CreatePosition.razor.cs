using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Pages.Admin.Company
{
    public partial class CreatePosition
    {
        private CreatePositionRequest _positionData = new();
        private string _message;

        private ElementReference _descriptionInput;

        private async Task HandleValidSubmit()
        {
            try
            {
                await companyService.CreatePositionAsync(_positionData);
                UriHelper.NavigateTo("/");
            }
            catch (ApiException<ErrorResponse> ex)
            {
                _message = ex.Result.Message;
            }
            catch (ApiException<OperationResultResponse> ex)
            {
                foreach (var error in ex.Result.Errors)
                {
                    _message = _message + " " + error;
                }
            }
            catch (ApiException ex)
            {
                _message = "Something went wrong";
            }
        }
    }
}
