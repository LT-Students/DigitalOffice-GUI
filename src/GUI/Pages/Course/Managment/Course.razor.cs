using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Course.Managment
{
  partial class Course
  {
    [Parameter]
    public CourseResponse CourseData { get; set; }

    private CreateExamRequest _request { get; set; } = new CreateExamRequest();

    private ElementReference _descriptionInput;
    private ElementReference _deadlineInput;

    protected override void OnParametersSet()
    {
      this.StateHasChanged();
    }

    private async Task SubmitValidExamAsync()
    {
      try
      {
        _request.CourseId = CourseData.Id;
        await _examService.CreateExamAsync(_request);
        _request = new();
      }
      catch (Exception ex)
      {
      }

      StateHasChanged();
    }

    private void OnSetExamAsync(Guid examId)
    {
      UriHelper.NavigateTo($"{Urls.ManageCourseExam}{examId}");
    }
  }
}
