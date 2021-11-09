using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Workspace
{
	public partial class Channels
	{
		[Parameter]
		public Body WorkspaceInfo { get; set; }

		private Body _workspace = new();
	}
}
