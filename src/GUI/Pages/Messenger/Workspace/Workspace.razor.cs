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

    private OperationResultResponseChannelInfo _channelInfo;

    protected override void OnParametersSet()
    {
      _channelInfo = null;
      StateHasChanged();
    }

    private async Task OnSetChannelAsync(Guid channelId)
    {
      try
      {
        _channelInfo = await _messageService.GetChannelAsync(channelId, 0, 20);
      }
      catch (Exception ex)
      {
        _channelInfo = null;
      }

      StateHasChanged();
    }
  }
}
