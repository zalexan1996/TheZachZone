using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TZZ.Common.Configuration;
using TZZ.Core.Shared;

namespace TZZ.Core;

public static class StartupExtensions
{
    public static void AddCore(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<ZachZoneCommand>();
        });
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.Configure<Security>(configuration.GetSection(nameof(Security)));
    }
}
