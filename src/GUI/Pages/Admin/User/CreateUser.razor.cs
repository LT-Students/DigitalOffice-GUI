﻿using LT.DigitalOffice.GUI.Services.ApiClients.CompanyService;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Pages.Admin.User
{
    public partial class CreateUser
    {
        private CreateUserRequest _userData = new();
        private CommunicationInfo _userCommunication = new();
        private DepartmentsResponse _departments;
        private ICollection<PositionResponse> _positions;
        private string _message;
        private List<float> _rateValues = new List<float> { 0.2f, 0.4f, 0.6f, 0.8f, 1 };

        private ElementReference _lastNameInput;
        private ElementReference _middleNameInput;
        private ElementReference _emailInput;
        private ElementReference _passwordInput;
        private ElementReference _startWorkingAtInput;
        private ElementReference _rateSelect;
        private ElementReference _departmentSelect;
        private ElementReference _positionSelect;
        private ElementReference _statusSelect;

        public CreateUser()
        {
            _userCommunication.Type = CommunicationType.Email;
            _userData.Communications = new List<CommunicationInfo>();
            _userData.Communications.Add(_userCommunication);
        }

        private async Task GeneratePasswordAsync()
        {
            _userData.Password = await userService.GeneratePasswordAsync();
        }

        private async Task HandleValidSubmitAsync()
        {
            try
            {
                await userService.CreateUserAsync(_userData);
                UriHelper.NavigateTo("/admin");
            }
            catch(Services.ApiClients.UserService.ApiException<Services.ApiClients.UserService.ErrorResponse> ex)
            {
                _message = ex.Result.Message;
            }
            catch(Exception ex)
            {
                //remove when spec reworked
                _message = ex.Message;
            }
        }

        protected async override void OnInitialized()
        {
            _positions = await companyService.GetPositionsAsync();
            _departments = await companyService.GetDepartmentsAsync();
            _userData.StartWorkingAt = DateTime.UtcNow;
            StateHasChanged();
        }
    }
}