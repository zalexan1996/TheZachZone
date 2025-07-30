using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using TZZ.Common.Configuration;

namespace TZZ.Common;

public static class StartupExtensions
{
  public static void AddCommon(this IServiceCollection services, ConfigurationManager configuration)
  {
    _ = TheZachZoneConfigurationBuilder.BuildConfiguration(configuration);
  }
}
