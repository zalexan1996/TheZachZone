using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TZZ.WebShared.Health;

public class HealthCheckResponseWriter
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
