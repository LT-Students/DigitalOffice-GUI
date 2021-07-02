using System;
using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ISessionStorageService _storage;
        private readonly ProjectServiceClient _projectServiceClient;

        public ProjectService(ISessionStorageService storage)
        {
            _storage = storage;
            _projectServiceClient = new ProjectServiceClient(new HttpClient());
        }

        public async Task<FindResponseProjectInfo> FindProjects(
            int skipCount,
            int takeCount,
            string shortName = null,
            string projectName = null,
            string departmentName = null)
        {
            FindResponseProjectInfo projectsResponse = null;
            try
            {
                var token = await _storage.GetItemAsync<string>(Consts.Token);

                projectsResponse =  await _projectServiceClient.FindProjectsAsync(token, projectName, shortName, departmentName, skipCount, takeCount);
            }
            catch (ApiException<ErrorResponse> exc)
            {
                // TODO add exception handler
                throw;
            }

            return projectsResponse;
        }

        public async Task<ProjectInfo> CreateProject(ProjectRequest request)
        {
            OperationResultResponseProjectInfo response = null;
            try
            {
                var token = await _storage.GetItemAsync<string>(Consts.Token);

                response =  await _projectServiceClient.CreateProjectAsync(request, token);
            }
            catch (ApiException<ErrorResponse> exc)
            {
                // TODO add exception handler
                throw;
            }

            return response.Body;
        }

        public async Task<FindResponseTaskProperty> GetTaskPropertiesAsync(
            int skipCount,
            int takeCount, 
            string name = null,
            Guid? authorId = null,
            Guid? projectId = null)
        {
            var token = await _storage.GetItemAsync<string>(Consts.Token);

            return await _projectServiceClient.FindTaskPropertiesAsync(token, name, authorId,projectId, skipCount, takeCount);
        }

        public async Task<ProjectResponse> GetProjectAsync(
            Guid projectId, 
            bool includeUsers = false, 
            bool includeFiles = false, 
            bool showNotActiveUsers = false)
        {
            var token = await _storage.GetItemAsync<string>(Consts.Token);

            return await _projectServiceClient.GetProjectAsync(token, projectId, includeUsers, showNotActiveUsers, includeFiles);
        }

        public async Task<OperationResultResponse> CreateTaskAsync(CreateTaskRequest request)
        {
            var token = await _storage.GetItemAsync<string>(Consts.Token);

            return await _projectServiceClient.CreateTaskAsync(request, token);
        }
    }
}