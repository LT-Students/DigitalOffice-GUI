using System;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace LT.DigitalOffice.GUI.Services
{
  public class ChatHub : IChatHub
  {
    private readonly ISessionStorageService _storage;
    private HubConnection? _hubConnection;

    public ChatHub(ISessionStorageService storage)
    {
      _storage = storage;
    }

    public async Task Connect(Guid channelId)
    {
      string t = await _storage.GetItemAsync<string>(Consts.AccessToken);
      Console.WriteLine(t);

      _hubConnection = new HubConnectionBuilder()
        .WithUrl("https://message.dev.ltdo.xyz/chatHub", options =>
        {
          options.Headers.Add("token", t);
        })
        .Build();
      /*hubConnection.On<string>("channelId", (ChannelId) =>
      {
        var encodedMsg = $"{user}: {message}";
        //messages.Add(encodedMsg);
        StateHasChanged();
      });*/
      await _hubConnection.StartAsync();

      await _hubConnection.InvokeAsync("JoinChannel", channelId.ToString());
    }
  }
}
