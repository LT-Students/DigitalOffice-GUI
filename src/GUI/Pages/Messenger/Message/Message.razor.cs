using System;
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

    protected override async Task OnInitializedAsync()
    {
      try
      {
        await _chatHub.Connect(ChannelId);
      }
      catch (Exception ex)
      {

      }
    }

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
      catch (ApiException<OperationResultResponse> ex)
      {
      }
      catch (Exception ex)
      {
      }
    }
  }
}
