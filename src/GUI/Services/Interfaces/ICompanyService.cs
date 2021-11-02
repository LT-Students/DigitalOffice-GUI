using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
  public interface ICompanyService
  {
    Task CreateDepartmentAsync(CreateDepartmentRequest request);

    Task CreatePositionAsync(CreatePositionRequest request);

    Task<FindResultResponseDepartmentInfo> FindDepartmentsAsync();

    Task<FindResultResponsePositionInfo> FindPositionsAsync();

    Task<FindResultResponseOfficeInfo> FindOfficesAsync();
  }
}
