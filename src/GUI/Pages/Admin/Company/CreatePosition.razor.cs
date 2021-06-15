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
                await companyService.CreatePosition(_positionData);
                UriHelper.NavigateTo("/admin");
            }
            catch (ApiException<ErrorResponse> ex)
            {
                _message = ex.Result.Message;
            }
            catch (Exception ex)
            {
                //remove when spec reworked
                _message = ex.Message;
            }
        }
    }
}
