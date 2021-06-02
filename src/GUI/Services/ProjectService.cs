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

        public ProjectService(ISessionStorageService storage)
        {
            _storage = storage;
        }

        public async Task<FindResponseProjectInfo> FindProjects(
            int skipCount,
            int takeCount,
            string shortName = null,
            string projectName = null,
            string departmentName = null)
        {
            var httpClient = new HttpClient();

            var projectService = new ProjectServiceClient(httpClient);

            FindResponseProjectInfo projectsResponse = null;
            try
            {
                var token = await _storage.GetItemAsync<string>(Consts.Token);

                projectsResponse =  await projectService.FindProjectsAsync(token, projectName, shortName, departmentName, skipCount, takeCount);
            }
            catch (ApiException<ErrorResponse> exc)
            {
                // TODO add exception handler
                throw;
            }

            return projectsResponse;
        }
    }
}