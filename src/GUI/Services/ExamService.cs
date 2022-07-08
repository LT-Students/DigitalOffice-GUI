using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ExamService;
using LT.DigitalOffice.GUI.Services.Interfaces;

namespace LT.DigitalOffice.GUI.Services
{
  public class ExamService : IExamService
  {
    private readonly RefreshTokenHelper _refreshToken;
    private readonly ISessionStorageService _storage;
    private readonly ExamServiceClient _client;

    public ExamService(
      ISessionStorageService storage,
      IAuthService authService)
    {
      _storage = storage;
      _refreshToken = new(authService, storage);
       _client = new ExamServiceClient(new HttpClient());
    }

    public async Task CreateCourseAsync(CreateCourseRequest request)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      await _client.CreateCourseAsync(request, token);
    }

    public async Task<FindResultResponseCourseInfo> FindCourseAsync()
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.FindCoursesAsync(token);
    }

    public async Task<OperationResultResponseCourseResponse> GetCourseAsync(Guid courseId)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.GetCourseAsync(courseId, token);
    }

    public async Task CreateExamAsync(CreateExamRequest request)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      await _client.CreateExamAsync(request, token);
    }

    public async Task CreateQuestionAsync(CreateQuestionRequest request)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      await _client.CreateQuestionAsync(request, token);
    }

    public async Task<FindResultResponseExamInfo> FindExamsAsync()
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.FindExamsAsync(token, 0, int.MaxValue);
    }

    public async Task<OperationResultResponseExamResponse> GetExamAsync(Guid examId)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.GetExamAsync(examId, token);
    }

    public async Task<OperationResultResponse> CreateUserAnswerAsync(List<CreateUserAnswerRequest> request)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.CreateUserAnswerAsync(request, token);
    }

    public async Task<OperationResultResponseUserExamResponse> GetUserExamAsync(Guid examId)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.GetUserExamAsync(examId, token);
    }

    public async Task<FindResultResponseUserExamInfo> FindUserCourseExamsAsync(Guid courseId)
    {
      await _refreshToken.RefreshAsync();
      var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

      return await _client.FindUserCourseExamsAsync(courseId, token);
    }
  }
}
