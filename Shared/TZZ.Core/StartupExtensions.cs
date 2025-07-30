using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TZZ.Common.Configuration;
using TZZ.Core.Common;
using TZZ.Core.Common.Behaviors;
using TZZ.Core.Common.Services;
using TZZ.Core.PocketPersona.ActivityLog.Services;

namespace TZZ.Core;

public static class StartupExtensions
{
  public static void AddCore(this IServiceCollection services, IConfigurationRoot configuration)
  {
    services.AddMediatR(x =>
    {
      x.RegisterServicesFromAssemblyContaining<ZachZoneCommandResponse>();
      x.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    services.AddTransient<ResourceService>();
    services.AddTransient<ActivityLogService>();
    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
    services.Configure<AppSecurity>(configuration.GetSection(nameof(AppSecurity)));
    services.Configure<TelemetrySettings>(configuration.GetSection(nameof(TelemetrySettings)));

    services.AddSingleton(typeof(ITelemetryService<>), typeof(TelemetryService<>));
  }
}
