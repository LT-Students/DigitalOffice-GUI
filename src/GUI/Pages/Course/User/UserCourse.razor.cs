using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Course.User
{
  public partial class UserCourse
  {
    [Parameter]
    public Guid Id { get; set; }

    private List<CourseInfo> _userCourses = new();

    private List<UserExamInfo> _userExams = new();

    protected async override void OnInitialized()
    {
      try
      {
        _userCourses = (await _examService.FindCourseAsync(true)).Body.ToList();

        if (_userCourses is not null && _userCourses.Any())
        {
          await OnSetCourseAsync(_userCourses.FirstOrDefault().Id);
        }
        else
        {
          StateHasChanged();
        }
      }
      catch (Exception ex)
      {

      }
    }

    private async Task OnSetCourseAsync(Guid courseId)
    {
      try
      {
        _userExams = (await _examService.FindUserCourseExamsAsync(courseId)).Body.ToList();
      }
      catch(Exception ex)
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

