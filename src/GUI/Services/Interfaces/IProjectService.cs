using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IProjectService
    {
        Task<FindResponseProjectInfo> FindProjects(
            int skipCount,
            int takeCount, 
            string shortName = null,
            string projectName = null,
            string departmentName = null);

        Task<ProjectInfo> CreateProject(ProjectRequest request);

        Task<FindResponseTaskInfo> GetTasksAsync(
            int skipCount,
            int takeCount,
            int? number = null,
            Guid? projectId = null,
            Guid? assignedTo = null);
    }
}