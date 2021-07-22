using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task CreateDepartmentAsync(NewDepartmentRequest request);

        Task CreatePositionAsync(CreatePositionRequest request);

        Task<DepartmentsResponse> GetDepartmentsAsync();

        Task<ICollection<PositionResponse>> GetPositionsAsync();
    }
}