using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Course.User
{
  partial class UserExam
  {
    [Parameter]
    public Guid Id { get; set; }

    private bool _submitButtonDisabled = true;

    private UserExamResponse _examData { get; set; }

    private Dictionary<Guid, Guid> _questionsAnswers { get; set; } = new();

    protected async override void OnInitialized()
    {
      try
      {
        _examData = (await _examService.GetUserExamAsync(Id)).Body;
      }
      catch (Exception ex)
      {
      }

      StateHasChanged();
    }

    public void SelectionChanged(Guid questionId, Guid answerId)
    {
      _questionsAnswers[questionId] = answerId;

      _submitButtonDisabled = _questionsAnswers.Count != _examData.UserQuestions.Count;
    }

    private async void SubmitAsync()
    {
      try
      {
        OperationResultResponse response = await _examService.CreateUserAnswerAsync(
          _questionsAnswers.Select(i => new CreateUserAnswerRequest() { QuestionId = i.Key, AnswerId = i.Value }).ToList());

        if ((bool)response.Body == true)
        {
          UriHelper.NavigateTo(Urls.UserCourse);
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}


