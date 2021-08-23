using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<OperationResultResponse> CreateWorkspace(WorkspaceRequest request);
    }
}