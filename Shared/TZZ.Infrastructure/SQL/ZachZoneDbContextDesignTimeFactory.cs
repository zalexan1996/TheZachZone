using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TZZ.Common.Configuration;

namespace TZZ.Infrastructure.SQL;

public class ZachZoneDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ZachZoneDbContext>
{
  public ZachZoneDbContext CreateDbContext(string[] args)
  {
    var cfg = TheZachZoneConfigurationBuilder.BuildConfiguration()
      .Build();

    var builder = new DbContextOptionsBuilder<ZachZoneDbContext>();

    builder.UseSqlServer(cfg.GetConnectionString("Default"));

    return new ZachZoneDbContext(builder.Options);
  }
}