using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Message
{
  public partial class Message
  {
    [Parameter]
    public MessageInfo MessageData { get; set; }
  }
}
