using GUI.Pages.Auth;
using LT.DigitalOffice.GUI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Blazored.SessionStorage;

namespace LT.DigitalOffice.GUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddBlazoredSessionStorage();

            await builder.Build().RunAsync();
        }
    }
}
