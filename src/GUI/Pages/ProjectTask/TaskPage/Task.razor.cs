using System;
using Microsoft.AspNetCore.Components;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;

namespace LT.DigitalOffice.GUI.Pages.Project.TaskPage
{
    public partial class Task
    {

        private TasksResponse _task;

        [Inject]
        private IProjectService _ProjectService { get; set; }

        // protected override async Task OnInitialized()
        // {
        //     // var taskResponse = await _ProjectService.GetTaskAsync(taskId);

        //     // _task = taskResponse.Body;
        // }
    }
}