using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using LT.DigitalOffice.GUI.Services.ApiClients.AuthService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LT.DigitalOffice.GUI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ISessionStorageService _storage;
        private readonly CompanyServiceClient _companyServiceClient;
        private readonly AuthServiceClient _authServiceClient;


        public CompanyService(ISessionStorageService storage)
        {
            _storage = storage;
            _companyServiceClient = new CompanyServiceClient(new HttpClient());
            _authServiceClient = new AuthServiceClient(new HttpClient());
        }

        public async Task<List<DepartmentInfo>> GetDepartments()
        {
            DepartmentsResponse response = null;
            try
            {
                //var token = await _storage.GetItemAsync<string>(Consts.Token);
                var request = new AuthenticationRequest
                {
                    LoginData = "admin",
                    Password = "%4fgT1_3ioR"
                };

                var response1 = await _authServiceClient.LoginAsync(request);

                response =  await _companyServiceClient.GetDepartmentsAsync(response1.Token);
            }
            catch (Exception exc)
            {
                // TODO add exception handler
                throw;
            }

            return response.Departments.ToList();
        }
    }
}
﻿using Blazored.SessionStorage;
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
                _token = await _storage.GetItemAsync<string>(Consts.Token);
                var response = await _client.AddPositionAsync(request, _token);

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