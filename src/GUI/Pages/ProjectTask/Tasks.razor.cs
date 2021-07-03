using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;

namespace LT.DigitalOffice.GUI.Pages.ProjectTask
{
    public partial class Tasks
    {
        private const int TakeCount = 7;
        public const string UserLinkStyle = "text-decoration: none; color: #0b5ed7;";

        private int _totalCount;
        private int _skipCount;
        private List<TaskInfo> _tasks;

        [Inject]
        private IProjectService _ProjectService { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            _tasks = new();

            await GetTasks();

        }

        private async Task GetTasks()
        {
            var tasksResponse = await _ProjectService.GetTasksAsync(skipCount: 0, takeCount: int.MaxValue);

            _tasks.AddRange(tasksResponse.Body.ToList());
            
            _totalCount = tasksResponse.TotalCount;
            _skipCount ++;
        }
    }
}