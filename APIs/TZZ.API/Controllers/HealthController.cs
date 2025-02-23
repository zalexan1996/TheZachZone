﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using TZZ.Common.Shared.Enums;

namespace TZZ.API.Controllers;

[ApiController]
[Route("health")]
[Authorize(Policy = ZachZoneConstants.Policies.Admin)]
public class HealthController(HealthCheckService health) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        HealthReport report = await health.CheckHealthAsync();
        var response = new
        {
            report.Entries,
            Summary = new
            {
                StatusCode = report.Status,
                StatusString = report.Status.ToString(),
                report.TotalDuration,
            }
        };

        return report.Status == HealthStatus.Healthy 
            ? Ok(response) 
            : StatusCode((int)HttpStatusCode.ServiceUnavailable, response);
    }
}
