using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
  public interface IProjectService
  {
    Task<ProjectResponse> GetProjectAsync(
      Guid projectId,
      bool includeUsers = false,
      bool includeFiles = false,
      bool showNotActiveUsers = false);

    Task<FindResponseProjectInfo> FindProjects(
      int skipCount,
      int takeCount,
      Guid? departmentId = null);

    Task<ProjectInfo> CreateProject(ProjectRequest request);

    Task<OperationResultResponse> CreateTaskAsync(CreateTaskRequest request);

    Task<FindResponseTaskProperty> GetTaskPropertiesAsync(
      int skipCount,
      int takeCount,
      string name = null,
      Guid? authorId = null,
      Guid? projectId = null);

    Task<FindResponseTaskInfo> FindTasksAsync(
      int skipCount,
      int takeCount,
      int? number = null,
      Guid? projectId = null,
      Guid? statusId = null,
      Guid? assignedTo = null,
      bool onlyAuthorizedUser = false);

    Task<OperationResultResponseTaskResponse> GetTaskAsync(Guid taskId);

    Task<OperationResultResponse> EditTaskAsync(IEnumerable<PatchDocument> body, Guid taskId);
  }
}
