using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace LT.DigitalOffice.GUI.Pages.Messenger.Channel
{
  public partial class Channel
  {
    [Parameter]
    public OperationResultResponseChannelInfo ChannelData { get; set; }

    private HubConnection? _hubConnection;

    protected override async void OnParametersSet()
    {
      if (ChannelData is not null)
      {
        try
        {
          string token = await _storage.GetItemAsync<string>(Consts.AccessToken);

          _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:9810/chatHub"/*"https://message.dev.ltdo.xyz/chatHub"*/, options =>
            {
              options.Headers.Add("token", token);
            })
            .AddJsonProtocol(options =>
            {
              options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            })
            .WithAutomaticReconnect()
            .Build();

          await _hubConnection.StartAsync();
          await _hubConnection.InvokeAsync("JoinChannel", ChannelData.Body.Id.ToString());

          _hubConnection.On<MessageInfo>(
            "ReceiveMessage",
            (Message) =>
            {
              Console.WriteLine($"Got message {Message.Content}");
              this.StateHasChanged();
            });
        }
        catch (Exception ex)
        {
        }
      }
    }
  }
}
