using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LT.DigitalOffice.GUI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ISessionStorageService _storage;
        private readonly CompanyServiceClient _client;
        private string _token;

        public CompanyService(ISessionStorageService storage)
        {
            _storage = storage;
            _client = new CompanyServiceClient(new HttpClient());
        }

        public async Task CreateDepartmentAsync(NewDepartmentRequest request)
        {
            _token = await _storage.GetItemAsync<string>(Consts.AccessToken);

            await _client.AddDepartmentAsync(request, _token);
        }

        public async Task CreatePositionAsync(CreatePositionRequest request)
        {
            _token = await _storage.GetItemAsync<string>(Consts.AccessToken);

            await _client.AddPositionAsync(request, _token);
        }

        public async Task<DepartmentsResponse> FindDepartmentsAsync()
        {
            _token = await _storage.GetItemAsync<string>(Consts.AccessToken);

            return await _client.FindDepartmentsAsync(_token);
        }

        public async Task<ICollection<PositionResponse>> FindPositionsAsync()
        {
            var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

            return await _client.FindPositionsAsync(token);
        }

        public async Task<FindOfficesResponse> FindOfficesAsync()
        {
            var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

            return await _client.FindOfficesAsync(token, 0, 100, null);
        }
    }
}
