using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<string> CreateDepartment(NewDepartmentRequest request);

        public Task<string> CreatePosition(CreatePositionRequest request);

        public Task<DepartmentsResponse> GetDepartments();

        public Task<ICollection<PositionResponse>> GetPositions();
    }
}
