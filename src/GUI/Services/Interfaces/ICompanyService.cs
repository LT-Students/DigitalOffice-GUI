using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<string> CreateDepartment(NewDepartmentRequest request);

        Task<string> CreatePosition(CreatePositionRequest request);

        Task<DepartmentsResponse> GetDepartments();

        Task<ICollection<PositionResponse>> GetPositions();
    }
}
