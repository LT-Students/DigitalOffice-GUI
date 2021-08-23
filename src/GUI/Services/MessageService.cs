using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using LT.DigitalOffice.GUI.Helpers;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using LT.DigitalOffice.GUI.Services.Interfaces;

namespace LT.DigitalOffice.GUI.Services
{
    public class MessageService : IMessageService
    {
        private readonly RefreshTokenHelper _refreshToken;
        private readonly MessageServiceClient _client;
        private readonly ISessionStorageService _storage;

        public MessageService(ISessionStorageService storage, IAuthService authService)
        {
            _storage = storage;
            _refreshToken = new(authService, storage);
            _client = new MessageServiceClient(new HttpClient());
        }

        public async Task<OperationResultResponse> CreateWorkspace(WorkspaceRequest request)
        {
            await _refreshToken.RefreshAsync();
            var token = await _storage.GetItemAsync<string>(Consts.AccessToken);

            return await _client.Create2Async(request, token);
        }
    }
}