using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;

namespace LT.DigitalOffice.GUI.Pages.ProjectTask.TaskWindow
{
    public partial class TaskModalWindow
    {
        private bool _isEditTask;
        private TaskResponse _task;

        [Inject]
        private IProjectService _ProjectService { get; set; }

        protected override void OnInitialized()
        {
            _task = new();
        }

        public async Task GetTaskAsync(Guid taskId)
        {
            var taskResponse = await _ProjectService.GetTaskAsync(taskId);
            _task = taskResponse.Body;

            StateHasChanged();
        }
    }
}