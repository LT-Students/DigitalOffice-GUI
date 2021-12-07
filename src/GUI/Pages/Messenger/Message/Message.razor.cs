using System;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Message
{
  public partial class Message
  {
    [Parameter]
    public Guid ChannelId { get; set; }

    private CreateMessageRequest _request = new();

    private HubConnection? _hubConnection;

    protected override async Task OnInitializedAsync()
    {
      string token = await _storage.GetItemAsync<string>(Consts.AccessToken);
      Console.WriteLine(token);

      try
      {
        _hubConnection = new HubConnectionBuilder()
          .WithUrl("https://message.dev.ltdo.xyz/chatHub", options =>
          {
            //options.AccessTokenProvider();
            options.Headers.Add("token", token);
          })
          .Build();
      }
      catch(Exception ex)
      {

      }

      /*hubConnection.On<string>("channelId", (ChannelId) =>
      {
        var encodedMsg = $"{user}: {message}";
        //messages.Add(encodedMsg);
        StateHasChanged();
      });*/
      _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
      {
        Console.WriteLine($"Got message {message} from user {user}");
        this.StateHasChanged();
      });

      await _hubConnection.StartAsync();
      await _hubConnection.InvokeAsync("JoinChannel", ChannelId.ToString());
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
