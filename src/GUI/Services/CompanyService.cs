using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Services.Client.Company;
using LT.DigitalOffice.GUI.Services.Interfaces;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ISessionStorageService _storage;
        private readonly CompanyServiceClient _client = new CompanyServiceClient(new System.Net.Http.HttpClient());
        private string _token;
        public CompanyService(ISessionStorageService storage)
        {
            _storage = storage;
        }

        public async Task<string> CreateDepartment(NewDepartmentRequest request)
        {
            try
            {
                _token = await _storage.GetItemAsync<string>("Token");
                var response = await _client.AddDepartmentAsync(request, _token);

                return "Successfully created";
            }
            catch (Client.Company.ApiException<Client.Company.ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
        }

        public async Task<string> CreatePosition(CreatePositionRequest request)
        {
            try
            {
                _token = await _storage.GetItemAsync<string>("Token");
                var response = await _client.AddPositionAsync(request, _token);

                return "Successfully created";
            }
            catch (Client.Company.ApiException<Client.Company.ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
        }
    }
}

