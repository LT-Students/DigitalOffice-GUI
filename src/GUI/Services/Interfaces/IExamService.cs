using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
  public interface IExamService
  {
    Task CreateCourseAsync(CreateCourseRequest request);

    Task CreateExamAsync(CreateExamRequest request);

    Task CreateQuestionAsync(CreateQuestionRequest request);

    Task<FindResultResponseCourseInfo> FindCourseAsync(bool usercourses = false);

    Task<OperationResultResponseCourseResponse> GetCourseAsync(Guid courseId);

    Task<FindResultResponseExamInfo> FindExamsAsync();

    Task<OperationResultResponseExamResponse> GetExamAsync(Guid examId);

    Task<OperationResultResponse> CreateUserAnswerAsync(List<CreateUserAnswerRequest> request);

    Task<OperationResultResponseUserExamResponse> GetUserExamAsync(Guid examId);

    Task<FindResultResponseUserExamInfo> FindUserCourseExamsAsync(Guid courseId);

    Task<OperationResultResponse> CreateUserCourseAsync(Guid courseId, Guid userId);
  }
}
