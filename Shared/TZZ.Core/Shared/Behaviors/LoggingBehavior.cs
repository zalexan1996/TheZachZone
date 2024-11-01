using MediatR;
using Microsoft.Extensions.Logging;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ITelemetryService<LoggingBehavior<TRequest, TResponse>> telemetryService)
        : IPipelineBehavior<TRequest, TResponse> 
            where TRequest : notnull
            where TResponse : ZachZoneCommandResponse
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var activity = telemetryService.StartActivity(ActivityName);
        
        var response = await next();

        var castedResponse = response as ZachZoneCommandResponse;
        if (castedResponse != null && castedResponse.Errors.Any())
        {
            telemetryService.LogError("{Command} failed with errors: {Errors}", typeof(TRequest).Name, castedResponse.Errors);
        }

        castedResponse?.Infos.ToList().ForEach(x => activity?.AddTag(x.Key, $"Info: {x.Value}"));
        castedResponse?.Warnings.ToList().ForEach(x => activity?.AddTag(x.Key, $"Warning: {x.Value}"));
        castedResponse?.Errors.ToList().ForEach(x => activity?.AddTag(x.Key, $"Error: {x.Value}"));

        return response;
    }

    public string ActivityName => $"MediatR Command: {typeof(TRequest).Name}";
}
