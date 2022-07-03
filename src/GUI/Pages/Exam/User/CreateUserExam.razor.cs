using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Exam.User
{
  public partial class CreateUserExam
  {
    [Parameter]
    public Guid Id { get; set; }

    private bool _showAnswers = false;
    private bool _showAnswersButhonAvailability = false;
    private bool _submitButtonDisabled = true;

    private ExamInfo _examInfo { get; set; }
    private OperationResultResponseExamResponse _examData { get; set; }

    private Dictionary<Guid, Guid> _questionsAnswers { get; set; } = new();

    protected async override void OnInitialized()
    {
      try
      {
        _examData = await _examService.GetExamAsync(Id);

        if (Guid.TryParse(await Storage.GetItemAsync<string>(Consts.UserId), out Guid userId))
        {
          _showAnswersButhonAvailability = _examData?.Body?.Exam.CreatorInfo?.Id == userId;
        }
      }
      catch (Exception ex)
      {
      }

      StateHasChanged();
    }

    public void SelectionChanged(Guid questionId, Guid answerId)
    {
      _questionsAnswers[questionId] = answerId;

      _submitButtonDisabled = _questionsAnswers.Count != _examData.Body.Questions.Count;
    }

    public void ShowAnswersChanged()
    {
      _showAnswers = !_showAnswers;
    }

    private async void SubmitAsync()
    {
      try
      {
        OperationResultResponse response = await _examService.CreateUserAnswerAsync(
          _questionsAnswers.Select(i => new CreateUserAnswerRequest() { QuestionId = i.Key, AnswerId = i.Value }).ToList());

        if ((bool)response.Body == true)
        {
          UriHelper.NavigateTo("/user/exams");
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}


