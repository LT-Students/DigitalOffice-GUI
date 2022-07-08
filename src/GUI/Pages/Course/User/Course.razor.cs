using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Course.User
{
  public partial class Course
  {
    [Parameter]
    public Guid Id { get; set; }

    private List<UserExamInfo> _userExams = new();

    protected async override void OnInitialized()
    {
      try
      {
        _userExams = (await _examService.FindUserCourseExamsAsync(Guid.Empty)).Body.ToList();
      }
      catch (Exception ex)
      {

      }

      StateHasChanged();
    }

    private void OnSetExamAsync(Guid examId)
    {
      UriHelper.NavigateTo($"{Urls.UserCourseExam}{examId}");
    }
  }
}

