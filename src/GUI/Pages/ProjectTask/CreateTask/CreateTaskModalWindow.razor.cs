using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LT.DigitalOffice.GUI.Services.Interfaces;
using LT.DigitalOffice.GUI.Services.ApiClients.ProjectService;

namespace LT.DigitalOffice.GUI.Pages.ProjectTask.CreateTask
{
    public partial class CreateTaskModalWindow
    {
        private List<ProjectInfo> _projects;
        //private CreateTaskRequest _taskRequest;

        [Inject]
        private IProjectService _projectService { get; set; }

        [Inject]
        private IUserService _userService { get; set; }

        protected override void OnInitialized()
        {
            _projects = _projects ?? new();
        }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var projectsResponse = await _projectService.FindProjects(0, int.MaxValue);

                _projects = projectsResponse.Body.ToList();

                StateHasChanged();
            }
        }
    }
}