using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TZZ.WebShared.Health;

public static class HealthCheckResponseWriter
{
  public static Task WriteResponse(HttpContext context, HealthReport report)
  {
    return context.Response.WriteAsJsonAsync(new
    {
      report.Entries,
      Summary = new
      {
        StatusCode = report.Status,
        StatusString = report.Status.ToString(),
        report.TotalDuration,
      }
    });
  }
}
