using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.WebShared.Common;

public class ZachZoneExceptionHandler(ITelemetryService<ZachZoneExceptionHandler> telemetryService) : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    telemetryService.Logger.LogCommandException(
      commandName: httpContext.Request.Path.Value!,
      exception: exception.Message,
      innerException: exception.InnerException?.Message ?? string.Empty);

    await httpContext.Response.WriteAsJsonAsync(ZachZoneCommandResponse.Failure("Exception", exception.Message), cancellationToken);
    return true;
  }
}
