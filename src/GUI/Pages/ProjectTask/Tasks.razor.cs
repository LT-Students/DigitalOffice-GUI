using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;
using LT.DigitalOffice.GUI.Pages.ProjectTask.TaskWindow;

namespace LT.DigitalOffice.GUI.Pages.ProjectTask
{
    public partial class Tasks
    {
        private const int TakeCount = 7;
        public const string UserLinkStyle = "text-decoration: none; color: #0b5ed7;";

        private Guid _taskId;
        private int _totalCount;
        private int _skipCount;
        private List<TaskInfo> _tasks;
        private TaskModalWindow _taskModalWindow;

        [Inject]
        private IProjectService _ProjectService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            _tasks = new();

            await GetTasks();
        }

        private async Task GetTasks()
        {
            var tasksResponse = await _ProjectService.FindTasksAsync(skipCount: 0, takeCount: TakeCount);

            _tasks.AddRange(tasksResponse.Body.ToList());
            
            _totalCount = tasksResponse.TotalCount;
            _skipCount ++;
        }

        private string GetTaskTypeStyle(string typeName)
        {
            if (string.Equals("Feature", typeName))
            {
                return "color: green;";
            }
            else if (string.Equals("Bug", typeName))
            {
                return "color: red;";
            }
            else
            {
                return "color: blue";
            }
        }

        private void GetGuid(Guid taskId)
        {
            _taskId = taskId;
        }
    }
}