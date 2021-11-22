using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Workspace
{
  public partial class Workspace
  {
    [Parameter]
    public OperationResultResponseWorkspaceInfo WorkspaceData { get; set; }

    private OperationResultResponseChannelInfo _channelInfo = new();

    private async Task OnSetChannelAsync(Guid channelId)
    {
      try
      {
        _channelInfo = await _messageService.GetChannelAsync(channelId, 0, 20);
      }
      catch (Exception ex)
      {

      }
    }
  }
}
