using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TZZ.Common.Interfaces;
using TZZ.Infrastructure.SQL;

namespace TZZ.Infrastructure;

public static class StartupExtensions
{
  public static void AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
  {
    services.AddDbContext<IDatabaseService, ZachZoneDbContext>(x =>
    {
      var dbType = Environment.GetEnvironmentVariable("DB_TYPE");

      if (dbType == "SqlServer" || true)
      {
        x.UseSqlServer(configuration.GetConnectionString("Default"));
      }
      else
      {
        x.UseInMemoryDatabase("TheZachZone");
      }
    });

  }
}
