OpenTelemetry (OTEL) is a framework that generates, manages and exports telemetry data (traces, metrics, and logs).


> [!tip] System.Diagnostics
> OTEL's tracing uses the System.Diagnostics constructs like `Activity` and `ActivitySource`.  It will be beneficial to learn all about it [here](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics?view=net-8.0)

### Terms
**Observability** - The ability to understand the internal state of a system by examining its outputs.
**Telemetry** - 
**Trace** - 
**Metric** - 
**Log** -
**Distribution** - 
**Receiver** - 
**Resource** - The entity that produces telemetry. A process producing telemetry running in a container has a process name, a PID, an executing file, working memory, etc. All of these attributes could be included in the resource.


## Examples

``` c#

string service = "The-Zach-Zone";
// Register the OpenTelemetry service
services.AddOpenTelemetry()
	.ConfigureResource(r => r.AddService(service))
		.WithTracing(t => 
			t.AddAspNetCoreInstrumentation()
				.AddConsoleExporter()
				.AddSource(service))
		.WithMetrics(m => 
			m.AddAspNetCoreInstrumentation()
				.AddConsoleExporter()
				.AddMeter(service))
		.WithLogging(l => l.AddConsoleExporter());

// Configures the logging provider.
builder.Logging.AddOpenTelemetry(o =>
{
	var resourceBuilder = ResourceBuilder.CreateDefault()
							.AddService(service);
	
	o.SetResourceBuilder(resourceBuilder).AddConsoleExporter();
});
```

## Packages
**OpenTelemetry.Extensions.Hosting**
Provides extension methods for managing OTEL tracing (TraceProvider) and metrics (MetricProvider). 

The [AddOpenTelemetry](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Extensions.Hosting/OpenTelemetryServicesExtensions.cs) extension method, for instance, just registers the `TelemetryHostedService` service and returns an [OpenTelemetryBuilder](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Extensions.Hosting/OpenTelemetryBuilder.cs) for configuration.

**OpenTelemetry.Instrumentation.AspNetCore**

**OpenTelemetry.Exporter.Console**

## External Readings
- [The Specification](https://opentelemetry.io/docs/specs/otel)
- [OTLP](https://opentelemetry.io/docs/specs/otlp/)
- [.NET SDK](https://opentelemetry.io/docs/languages/net/)
- [System.Diagnostics](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics?view=net-8.0)