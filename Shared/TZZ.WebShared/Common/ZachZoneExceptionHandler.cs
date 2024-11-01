using Azure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Behaviors;
using TZZ.Core.Shared.Services;
namespace TZZ.WebShared.Common;

public class ZachZoneExceptionHandler(ITelemetryService<ZachZoneExceptionHandler> telemetryService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
    {

        telemetryService.LogError("'{Command}' threw an exception: {Exception}, {InnerException}", httpContext.Request.Path, ex.Message, ex.InnerException?.Message);
        await httpContext.Response.WriteAsJsonAsync(ZachZoneCommandResponse.Failure("Exception", ex.Message));
        return true;
    }
}
