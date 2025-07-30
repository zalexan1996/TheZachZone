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

    var assemblyLocation = typeof(AppSettings).Assembly.Location;
    var appSettingsJson = Path.Combine(Path.GetDirectoryName(assemblyLocation)!, "appsettings.json");
    config.AddJsonFile(appSettingsJson, optional: false);
    config.AddUserSecrets<AppSettings>();
    return config;
  }
}