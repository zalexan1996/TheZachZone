using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TZZ.Common.Configuration;

namespace TZZ.Common;

public static class StartupExtensions
{
    public static void AddCommon(this IServiceCollection services, ConfigurationManager configuration)
    {
        var config = TheZachZoneConfigurationBuilder.BuildConfiguration(configuration);
    }
}
