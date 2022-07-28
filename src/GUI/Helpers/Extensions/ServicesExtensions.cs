using LT.DigitalOffice.GUI.Services;
using LT.DigitalOffice.GUI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LT.DigitalOffice.GUI.Helpers.Extensions
{
  public static class ServicesExtensions
  {
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      if (services is null)
      {
        return services;
      }

      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IProjectService, ProjectService>();
      services.AddScoped<IExamService, ExamService>();
      services.AddScoped<IMessageService, MessageService>();
      services.AddScoped<IChatHub, ChatHub>();

      return services;
    }
  }
}
