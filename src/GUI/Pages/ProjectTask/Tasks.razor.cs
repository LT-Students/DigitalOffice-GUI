using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LT.DigitalOffice.GUI.Models.Filters;
using Microsoft.AspNetCore.Components.Web;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;

namespace LT.DigitalOffice.GUI.Pages.ProjectTask
{
    public partial class Tasks
    {
        private const int TakeCount = 5;
        public const string UserLinkStyle = "text-decoration: none; color: #0b5ed7;";

        private int _totalCount;
        private FindResponseTaskInfo _tasksResponse;

        [Inject]
        private IProjectService _ProjectService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            //_tasksResponse = await _ProjectService.FindTasksAsync(skipCount: 0, takeCount: int.MaxValue);

            _tasksResponse = new();
            _tasksResponse.Body = new List<TaskInfo>();
            _totalCount = TakeCount;

           for (int i=0; i <= _totalCount; i++)
           
            _tasksResponse.Body.Add(
                new TaskInfo
                {
                    Id = Guid.NewGuid(),
                    Name = "Create model",
                    TypeName = "Feature",
                    PlannedMinutes = 120,
                    StatusName = "In progress",
                    PriorityName = "Normal",
                    Project = new ProjectTaskInfo{
                        ProjectId = Guid.NewGuid(),
                        ShortName = "Digital Office"
                    },
                    Number = 130,
                    AssignedTo = new UserTaskInfo
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Evgeniy",
                        LastName = "Malkin"
                    },
                    Author = new UserTaskInfo
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Ivan",
                        LastName = "Ivanov"
                    }
                }
            );
            
            _tasksResponse.TotalCount = _totalCount;
        }
    }
}