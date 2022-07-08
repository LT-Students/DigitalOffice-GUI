using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using LT.DigitalOffice.GUI.Shared;

namespace LT.DigitalOffice.GUI.Pages.Course.Managment
{
  partial class Courses
  {
    private ModalWindow _modalWindow { get; set; }
    private string _modalTitle { get; set; }

    private List<CourseInfo> _courses = new();

    public CourseResponse CourseData;

    protected async override void OnInitialized()
    {
      try
      {
        _courses = (await _examService.FindCourseAsync()).Body.ToList();
      }
      catch (Exception ex)
      {

      }

      StateHasChanged();
    }

    private async Task OnSetCourseAsync(Guid courseId)
    {
      try
      {
        CourseData = (await _examService.GetCourseAsync(courseId)).Body;
      }
      catch(Exception ex)
      {

      }
      
      StateHasChanged();
    }
  }
}
