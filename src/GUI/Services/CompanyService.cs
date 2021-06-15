using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Helpers;
using System;

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
            _client = new CompanyServiceClient(new System.Net.Http.HttpClient());
        }

        public async Task CreateDepartment(NewDepartmentRequest request)
        {
            _token = await _storage.GetItemAsync<string>(Consts.Token);

            await _client.AddDepartmentAsync(request, _token);
        }

        public async Task CreatePosition(CreatePositionRequest request)
        {
            _token = await _storage.GetItemAsync<string>(Consts.Token);

            await _client.AddPositionAsync(request, _token);
        }

        public async Task<DepartmentsResponse> GetDepartments()
        {
            _token = await _storage.GetItemAsync<string>(Consts.Token);

            return await _client.GetDepartmentsAsync(_token);
        }

        public async Task<ICollection<PositionResponse>> GetPositions()
        {
            _token = await _storage.GetItemAsync<string>(Consts.Token);

            return await _client.GetPositionsListAsync(_token);
        }
    }
}