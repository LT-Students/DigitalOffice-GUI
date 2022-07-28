using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
  public interface IUserService
  {
    Task<bool> IsAdminAsync();

    Task<string> GetUserNameAsync();

    Task<OperationResultResponse> CreateUserAsync(CreateUserRequest request);

    Task<OperationResultResponseUserResponse> GetAuthorizedUserAsync(bool includeCommunications = true, bool includeProjects = false);

    Task<OperationResultResponseCredentialsResponse> CreateCredentialsAsync(CreateCredentialsRequest request);

    Task<string> GeneratePasswordAsync();

    Task<FindResultResponseUserInfo> FindUsersAsync(int skipCount, int takeCount, Guid? departmentId);
  }
}
