using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
  public interface IExamService
  {
    Task CreateExamAsync(CreateExamRequest request);

    Task<FindResultResponseExamInfo> FindExamsAsync();

    Task<OperationResultResponseExamResponse> GetExamAsync(Guid examId);

    Task<OperationResultResponse> CreateUserAnswerAsync(List<CreateUserAnswerRequest> request);

    Task<OperationResultResponseUserExamResponse> GetUserExamAsync(Guid examId);
  }
}
