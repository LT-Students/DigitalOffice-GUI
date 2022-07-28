using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Course.Managment
{
  partial class Exam
  {
    [Parameter]
    public Guid Id { get; set; }

    private bool _submitButtonDisabled = true;
    private ExamResponse _examData { get; set; }
    private CreateQuestionRequest _request { get; set; } = new();

    private List<CreateAnswerRequest> _answers = new();

    protected async override void OnInitialized()
    {
      try
      {
        _examData = (await _examService.GetExamAsync(Id)).Body;
      }
      catch (Exception ex)
      {
      }

      StateHasChanged();
    }

    private async Task SubmitValidQuestionAsync()
    {
      try
      {
        _request.ExamId = Id;
        _request.Answers = _answers;
        await _examService.CreateQuestionAsync(_request);

        _examData = (await _examService.GetExamAsync(Id)).Body;
      }
      catch (Exception ex)
      {
      }

      StateHasChanged();
    }

    private void AddAnswer()
    {
      _answers.Add(new());
      StateHasChanged();
    }
  }
}
