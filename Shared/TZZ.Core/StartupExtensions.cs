using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TZZ.Common.Configuration;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Behaviors;
using TZZ.Core.Shared.Services;

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
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.Configure<Security>(configuration.GetSection(nameof(Security)));
        services.Configure<TelemetrySettings>(configuration.GetSection(nameof(TelemetrySettings)));

        services.AddSingleton(typeof(ITelemetryService<>), typeof(TelemetryService<>));
    }
}
