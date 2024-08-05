using Microsoft.Extensions.Configuration;

namespace TZZ.Common.Configuration;

public static class TheZachZoneConfigurationBuilder
{
    public static IConfigurationBuilder BuildConfiguration(IConfigurationBuilder? config = null)
    {
        if (config is null)
        {
            config = new ConfigurationManager();
        }

        var appSettingsJson = Path.Combine(Path.GetDirectoryName(typeof(AppSettings).Assembly.Location), "appsettings.json");
        config.AddJsonFile(appSettingsJson, optional: false);
        config.AddUserSecrets<AppSettings>();
        return config;
    }
}