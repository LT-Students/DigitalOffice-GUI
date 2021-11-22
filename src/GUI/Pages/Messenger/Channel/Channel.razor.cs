using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Channel
{
  public partial class Channel
  {
    [Parameter]
    public OperationResultResponseChannelInfo ChannelData { get; set; }
  }
}
