using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.GUI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ISessionStorageService _storage;
        private readonly CompanyServiceClient _client;

        public CompanyService(ISessionStorageService storage)
        {
            _storage = storage;
            _client = new CompanyServiceClient(new HttpClient());
        }

        public async Task<string> CreateDepartment(NewDepartmentRequest request)
        {
            try
            {
                var token = await _storage.GetItemAsync<string>(Consts.AccessToken);
                var response = await _client.AddDepartmentAsync(request, token);

                return "Successfully created";
            }
            catch (ApiException<ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
            catch (Exception ex)
            {
                //remove when spec reworked
                return ex.Message;
            }
        }

        public async Task<string> CreatePosition(CreatePositionRequest request)
        {
            try
            {
                var token = await _storage.GetItemAsync<string>(Consts.AccessToken);
                var response = await _client.AddPositionAsync(request, token);

                return "Successfully created";
            }
            catch (ApiException<ErrorResponse> ex)
            {
                return ex.Result.Message;
            }
            catch (Exception ex)
            {
                //remove when spec reworked
                return ex.Message;
            }
        }

        public async Task<DepartmentsResponse> GetDepartments()
        {
            try
            {
                var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

                return await _client.GetDepartmentsAsync(token);
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
                var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

                return await _client.GetPositionsListAsync(token);
            }
            catch(ApiException<ErrorResponse> ex)
            {
                //to do when spec changed
                return null;
            }
        }
    }
}
