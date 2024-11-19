using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Common.Shared.Interfaces;

namespace TZZ.WebShared.Health;

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
