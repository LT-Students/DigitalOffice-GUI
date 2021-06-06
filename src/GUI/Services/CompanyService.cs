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

        public async Task<string> CreateDepartment(NewDepartmentRequest request)
        {
            try
            {
                _token = await _storage.GetItemAsync<string>(Consts.Token);
                var response = await _client.AddDepartmentAsync(request, _token);

                return "Successfully created";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> CreatePosition(CreatePositionRequest request)
        {
            try
            {
                _token = await _storage.GetItemAsync<string>(Consts.Token);
                var response = await _client.AddPositionAsync(request, _token);

                return "Successfully created";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<DepartmentsResponse> GetDepartments()
        {
            try
            {
                _token = await _storage.GetItemAsync<string>(Consts.Token);
                return await _client.GetDepartmentsAsync(_token);
            }
            catch (ApiException<ErrorResponse> ex)
            {
                //to do when spec changed
                return null;
            }
        }

        public async Task<ICollection<PositionResponse>> GetPositions()
        {
            try
            {
                _token = await _storage.GetItemAsync<string>(Consts.Token);

                return await _client.GetPositionsListAsync(_token);
            }
            catch(ApiException<ErrorResponse> ex)
            {
                //to do when spec changed
                return null;
            }
        }
    }
}