using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
