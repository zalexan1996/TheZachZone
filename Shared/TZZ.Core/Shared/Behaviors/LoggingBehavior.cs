using MediatR;
using Microsoft.Extensions.Logging;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ITelemetryService<LoggingBehavior<TRequest, TResponse>> telemetryService, ICurrentUserService currentUserService)
        : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var activity = telemetryService.StartActivity(ActivityName);
        
        try
        {
            var response = await next();

            var castedResponse = response as ZachZoneCommand;
            if (castedResponse != null && castedResponse.Errors.Any())
            {
                telemetryService.LogError("{Command} failed with errors: {Errors}", typeof(TRequest).Name, castedResponse.Errors);
            }

            castedResponse?.Infos.ToList().ForEach(x => activity?.AddTag(x.Key, $"Info: {x.Value}"));
            castedResponse?.Warnings.ToList().ForEach(x => activity?.AddTag(x.Key, $"Warning: {x.Value}"));
            castedResponse?.Errors.ToList().ForEach(x => activity?.AddTag(x.Key, $"Error: {x.Value}"));



            return response;
        }
        catch (Exception ex)
        {
            telemetryService.LogError("{Command} threw an exception: {Exception}, {InnerException}", typeof(TRequest).Name, ex.Message, ex.InnerException?.Message);
            return default;
        }
    }

    public string ActivityName => $"MediatR Command: {typeof(TRequest).Name}";
}
