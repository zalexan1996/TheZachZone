using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;
using TZZ.Core.Common.Services;

namespace TZZ.WebShared.Health.Telemetry;

public sealed class ResourceTelemetryService(ResourceService resources) : IHostedService, IDisposable
{
  private Timer? Timer;

  private void RecordResourceUsage(object? state)
  {
    using var meter = new Meter("The-Zach-Zone", "1.0");

    meter.CreateCounter<double>("tzz_resources_mem_used", description: "Used memory")
      .Add(resources.UsedRam());

    meter.CreateCounter<double>("tzz_resources_mem_total", description: "Total memory")
      .Add(resources.TotalRam);

    meter.CreateCounter<double>("tzz_resources_cpu_used", description: "Used CPU")
      .Add(ResourceService.CpuUsage);

    meter.CreateCounter<double>("tzz_resources_disk_used", description: "Used disk space in bytes.")
      .Add(ResourceService.UsedDisk);

    meter.CreateCounter<double>("tzz_resources_disk_total", description: "Total disk space in bytes.")
      .Add(ResourceService.TotalDisk);
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    Timer = new Timer(RecordResourceUsage, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    Timer?.Change(Timeout.Infinite, 0);

    return Task.CompletedTask;
  }

  public void Dispose()
  {
    Timer?.Dispose();
  }
}
