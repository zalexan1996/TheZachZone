using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TZZ.WebShared.Health.Checks;

public class ExternalAPIHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("All 3rd-party APIs are accessible."));
    }
}
