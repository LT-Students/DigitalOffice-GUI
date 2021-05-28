using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.Client.ProjectService;

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
    }
}