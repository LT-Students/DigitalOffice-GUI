namespace LT.DigitalOffice.GUI.Pages
{
  public partial class Index
  {
    protected async override void OnInitialized()
    {
      UriHelper.NavigateTo("/user/exams");

      StateHasChanged();
    }
  }
}
