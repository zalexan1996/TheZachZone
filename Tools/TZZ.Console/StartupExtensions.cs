using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TZZ.Common.Configuration;
using TZZ.Console.Services;
using TZZ.Core.Shared.Services;
using TZZ.WebShared;

namespace TZZ.Console;

public static class StartupExtensions
{
    public static void AddConsole(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assemblyLocation = typeof(CurrentUserService).Assembly.Location;
        var appSettingsJson = Path.Combine(Path.GetDirectoryName(assemblyLocation)!, "appsettings.tzz-console.json");
        configuration.AddJsonFile(appSettingsJson, optional: false);
        configuration.AddUserSecrets<AppSettings>();

        services.AddWebShared(configuration);
        services.AddTransient<ICurrentUserService, CurrentUserService>();

    }
}
