using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using LT.DigitalOffice.GUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace LT.DigitalOffice.GUI.Pages.Messages.Workspace
{
    partial class EditWorkspaceModal
    {
        [Parameter]
        public Body Workspace { get; set; }

        private bool _isSuccessOperation;
        private string _messageUser;
        private List<PatchWorkspaceDocument> _requestBody;

        [Inject]
        private IMessageService MesageService { get; set; }

        protected override void OnInitialized()
        {
            _requestBody = new();
        }

        private void SetValueToPatchDocument(PatchWorkspaceDocumentPath path, object value)
        {
            PatchWorkspaceDocument patch = _requestBody.FirstOrDefault(x => x.Path == path);

            if (patch is null)
            {
                _requestBody.Add(
                    new PatchWorkspaceDocument
                    {
                        Op = PatchWorkspaceDocumentOp.Replace,
                        Path = path,
                        Value = value
                    }
                );
            }
            else
            {
                patch.Value = value;
            }
        }

        private async Task EditWorkspaceAsync()
        {
            _messageUser = null;

            if (!_requestBody.Any())
            {
                _messageUser = "The workspace has not changes";
                _isSuccessOperation = false;

                return;
            }

            try
            {
                OperationResultResponse _editWorkspaceResponse = await MesageService.EditWorkspaceAsync(Workspace.Id, _requestBody);

                _isSuccessOperation = (bool)_editWorkspaceResponse.Body;
                _messageUser = _isSuccessOperation ?
                    "Changes were saved successfully!" :
                    $"Something went wrong, please try again later.\nMessage: { _editWorkspaceResponse.Errors }";
            }
            catch (ApiException ex)
            {
                _isSuccessOperation = false;
                _messageUser = $"Something went wrong, please try again later.\nMessage: { ex.Response }";
            }
            catch (Exception)
            {
                _isSuccessOperation = false;
                _messageUser = $"Something went wrong, please try again later.";
            }

            _requestBody = new();
        }
    }
}