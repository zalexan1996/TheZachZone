using Microsoft.Extensions.Diagnostics.HealthChecks;
using TZZ.Common.Interfaces;

namespace TZZ.WebShared.Health.Checks;

public class DBConnectionHealthCheck(IDatabaseService dbContext) : IHealthCheck
{
  public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
  {
    var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);

    if (canConnect)
    {
      return HealthCheckResult.Healthy("Database connection is established.");
    }
    else
    {
      return HealthCheckResult.Unhealthy("Database is unavailable.");
    }
  }
}
