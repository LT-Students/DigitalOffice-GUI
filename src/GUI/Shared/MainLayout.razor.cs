using System;
using System.Threading.Tasks;
using LT.DigitalOffice.GUI.Helpers;

namespace LT.DigitalOffice.GUI.Shared
{
  partial class MainLayout
  {
    private Type _instanceBody;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      await Storage.SetItemAsync(Consts.PageUri, UriHelper.Uri);
    }
  }
}
