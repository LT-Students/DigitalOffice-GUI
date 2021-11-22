using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;

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
      catch (ApiException<OperationResultResponse> ex)
      {
        //_isSuccessOperation = false;
        //_infoMessage = $"Something went wrong, please try again later.\nMessage: { ex.Result.Errors }";
      }
      catch (Exception ex)
      {
        //_isSuccessOperation = false;
        //_infoMessage = ex.Message;
      }
    }
  }
}
