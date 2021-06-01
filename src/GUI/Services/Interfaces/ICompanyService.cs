using LT.DigitalOffice.GUI.Services.Client.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<string> CreateDepartment(NewDepartmentRequest request);

        public Task<string> CreatePosition(CreatePositionRequest request);

        //public Task<DepartmentsResponse> FindDepartment();

        public Task<ICollection<PositionResponse>> FindPosition();
    }
}
