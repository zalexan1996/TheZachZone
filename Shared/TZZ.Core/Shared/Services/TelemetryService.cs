using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using TZZ.Common.Configuration;

namespace TZZ.Core.Shared.Services;

public interface ITelemetryService<TCategoryName> : IDisposable
{
    public ILogger<TCategoryName> Logger { get;}
    public Activity? StartActivity(string name);

    public void Log(LogLevel logLevel, string? message, params object?[] args) => Logger.Log(logLevel, message, args);
    public void LogTrace(string? message, params object?[] args) => Logger.LogTrace(message, args);
    public void LogInformation(string? message, params object?[] args) => Logger.LogInformation(message, args);
    public void LogDebug(string? message, params object?[] args) => Logger.LogDebug(message, args);
    public void LogWarning(string? message, params object?[] args) => Logger.LogWarning(message, args);
    public void LogError(string? message, params object?[] args) => Logger.LogError(message, args);
    public void LogCritical(string? message, params object?[] args) => Logger.LogCritical(message, args);
}

public class TelemetryService<TCategoryName> : ITelemetryService<TCategoryName>
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
