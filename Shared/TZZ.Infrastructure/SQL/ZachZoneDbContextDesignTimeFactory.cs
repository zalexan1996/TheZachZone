using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using TZZ.Common.Configuration;

namespace TZZ.Infrastructure.SQL;

public class ZachZoneDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ZachZoneDbContext>
{
    public ZachZoneDbContext CreateDbContext(string[] args)
    {
        var cfg = TheZachZoneConfigurationBuilder.BuildConfiguration()
            .Build();

        var builder = new DbContextOptionsBuilder<ZachZoneDbContext>();


        var dbType = Environment.GetEnvironmentVariable("DB_TYPE");

        if (dbType == "SqlServer")
        {
            builder.UseSqlServer(cfg.GetConnectionString("Default"));
        }
        else
        {
            builder.UseInMemoryDatabase("TheZachZone");
        }

        return new ZachZoneDbContext(builder.Options);
    }
}