using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LT.DigitalOffice.GUI.Services.ApiClients.UserService;

namespace LT.DigitalOffice.GUI.Pages.Project.CreateProject.ProjectEmployees
{
    public partial class Employees
    {
        [Parameter]
        public List<UserInfo> Users { get; set; }

        [Parameter]
        public Dictionary<int, bool> HideEmployees { get; set; }

        [Parameter]
        public Dictionary<int, bool> StatesSelectedEmployees { get; set; }

        public List<UserInfo> AddFoundEmployees()
        {
            List<UserInfo> selectedEmployees = new();

            foreach (int key in StatesSelectedEmployees.Keys)
            {
                if (StatesSelectedEmployees[key])
                {
                    HideEmployees[key] = true;
                    selectedEmployees.Add(Users[key]);

                    StatesSelectedEmployees[key] = false;
                }
            }

            return selectedEmployees;
        }

        public List<Guid> RemoveSelectedEmployees(out List<UserInfo> employees)
        {
            List<Guid> userIds = new();

            for (int i = StatesSelectedEmployees.Count - 1; i >= 0; i--)
            {
                if (StatesSelectedEmployees[i])
                {
                    userIds.Add(Users[i].Id);
                    Users.RemoveAt(i);

                    StatesSelectedEmployees[i] = false;
                }
            }

            employees = Users;

            return userIds;
        }
    }
}
