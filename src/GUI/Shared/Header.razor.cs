using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Shared
{
  partial class Header
  {
    private string _userName = string.Empty;
    private bool _isAdmin = false;
    private UserWindow _userWindow;

    protected async override Task OnInitializedAsync()
    {
      _userName = await _userService.GetUserNameAsync();
      _isAdmin = await _userService.IsAdminAsync();

      StateHasChanged();
    }
  }
}
