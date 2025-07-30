using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using TZZ.Common.Configuration;

namespace TZZ.Core.Common.Services;

public interface ITelemetryService<TCategoryName> : IDisposable
{
  public ILogger<TCategoryName> Logger { get; }
  public Activity? StartActivity(string name);


}
public sealed class TelemetryService<TCategoryName> : ITelemetryService<TCategoryName>
{
  private readonly ActivitySource _activitySource;
  private readonly ILogger<TCategoryName> _logger;

  public TelemetryService(IOptions<TelemetrySettings> telemetryOptions, ILogger<TCategoryName> logger)
  {
    _activitySource = new ActivitySource(telemetryOptions.Value.ServiceName, telemetryOptions.Value.ServiceVersion);
    _logger = logger;
  }

  public void Dispose() => _activitySource.Dispose();

  public ActivitySource ActivitySource => _activitySource;

  public ILogger<TCategoryName> Logger => _logger;

  public Activity? StartActivity(string name) => ActivitySource.StartActivity(name);
}
