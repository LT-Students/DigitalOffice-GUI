using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Course.Managment
{
  partial class CreateCourse
  {
    private CreateCourseRequest _request = new();
    private string _message;
    private bool _isSuccessOperation;

    private ElementReference _descriptionInput;
    private ElementReference _startDateInput;
    private ElementReference _endDateInput;

    private async Task HandleValidSubmitAsync()
    {
      try
      {
        await _examService.CreateCourseAsync(_request);
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
