using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Pages.Admin.Company
{
    public partial class CreateDepartment
    {
        private NewDepartmentRequest _departmentData = new();
        private BaseDepartmentInfo _departmentInfo = new();
        private string _message;

        private ElementReference _descriptionInput;
        private ElementReference _directorSelect;

        public CreateDepartment()
        {
            _departmentData.Info = _departmentInfo;
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                await companyService.CreateDepartmentAsync(_departmentData);
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
