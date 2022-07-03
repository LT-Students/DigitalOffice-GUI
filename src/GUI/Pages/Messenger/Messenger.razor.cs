using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Services.ApiClients.MessageService;
using LT.DigitalOffice.GUI.Shared;

namespace LT.DigitalOffice.GUI.Pages.Messenger
{
  public partial class Messenger
  {
    private ModalWindow _modalWindow { get; set; }
    private List<ShortWorkspaceInfo> _workspaces = new();
    private OperationResultResponseExamInfo _workspaceInfo;

    protected async override void OnInitialized()
    {
      try
      {
        _workspaces = (await _messageService.FindWorkspaceAsync(0, int.MaxValue)).Body.ToList();
      }
      catch (Exception ex)
      {

      }

      StateHasChanged();
    }

    private async Task OnSetWorkspaceAsync(Guid workspaceId)
    {
      try
      {
        _workspaceInfo = await _messageService.GetWorkspaceAsync(workspaceId, true, true);
      }
      catch (Exception ex)
      {
        _workspaceInfo = null;
      }

      StateHasChanged();
    }

    private string StringHandler(string name)
    {
      if (name.Length > 15)
      {
        return string.Join("'", name.Split(" ").Select(w => w[0])).ToUpper();
      }

      return name;
    }
  }
}
