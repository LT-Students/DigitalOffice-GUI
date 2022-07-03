using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using LT.DigitalOffice.GUI.Shared;

namespace LT.DigitalOffice.GUI.Pages.Exam.User
{
  public partial class Exams
  {
    private ModalWindow _modalWindow { get; set; }
    private List<ExamInfo> _exams = new();

    protected async override void OnInitialized()
    {
      try
      {
        _exams = (await _examService.FindExamsAsync()).Body.ToList();
      }
      catch (Exception ex)
      {

      }

      StateHasChanged();
    }

    private async Task OnSetExamAsync(Guid examId)
    {
      UriHelper.NavigateTo($"/exam/user/create/{examId}");
    }
  }
}

