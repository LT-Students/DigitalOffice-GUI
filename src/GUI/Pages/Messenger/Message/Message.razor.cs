using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Message
{
  public partial class Message
  {
    [Parameter]
    public Guid ChannelId { get; set; }

    private CreateMessageRequest _request = new();


    private async Task SendMessageAsync()
    {
      try
      {
        _request.Status = StatusType.Sent;
        _request.ChannelId = ChannelId;
        OperationResultResponse response = await _messageService.CreateMessageAsync(_request);

        if (response.Status == OperationResultStatusType.FullSuccess)
        {
          _request.Content = "";
          return;
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}
