using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI
{
  public partial class App
  {
    protected override async Task OnInitializedAsync()
    {
      if (await AuthService.AuthorizeAsync())
      {
        return;
      }

      if (UriHelper.Uri.IndexOf("credentials") == -1)
      {
        UriHelper.NavigateTo("/login");
      }
    }
  }
}
