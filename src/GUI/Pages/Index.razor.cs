using LT.DigitalOffice.GUI.Helpers;

namespace LT.DigitalOffice.GUI.Pages
{
  public partial class Index
  {
    protected async override void OnInitialized()
    {
      UriHelper.NavigateTo(Urls.UserCourse);

      StateHasChanged();
    }
  }
}
