using MediatR;
using TZZ.Core.Common.Services;

namespace TZZ.Core.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
  (ITelemetryService<LoggingBehavior<TRequest, TResponse>> telemetryService)
    : IPipelineBehavior<TRequest, TResponse>
      where TRequest : notnull
      where TResponse : ZachZoneCommandResponse
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    using var activity = telemetryService.StartActivity(ActivityName);

    var response = await next(cancellationToken);

    var castedResponse = response as ZachZoneCommandResponse;
    if (castedResponse != null && castedResponse.Errors.Any())
    {
      telemetryService.Logger.LogCommandError(typeof(TRequest).Name, castedResponse.Errors);
    }

    castedResponse?.Infos.ToList().ForEach(x => activity?.AddTag(x.Key, $"Info: {x.Value}"));
    castedResponse?.Warnings.ToList().ForEach(x => activity?.AddTag(x.Key, $"Warning: {x.Value}"));
    castedResponse?.Errors.ToList().ForEach(x => activity?.AddTag(x.Key, $"Error: {x.Value}"));

    return response;
  }

  public string ActivityName => $"MediatR Command: {typeof(TRequest).Name}";
}
