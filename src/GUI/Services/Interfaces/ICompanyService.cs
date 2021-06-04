using System.Threading.Tasks;
using System.Collections.Generic;
using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<List<DepartmentInfo>> GetDepartments();
    }
}
