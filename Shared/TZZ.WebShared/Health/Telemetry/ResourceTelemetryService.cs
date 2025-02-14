using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Core.Shared.Services;

namespace TZZ.WebShared.Health.Telemetry;

public class ResourceTelemetryService(ITelemetryService<ResourceTelemetryService> telemetry, ResourceService resources) : IHostedService
{
    private Timer? Timer = null;

    private void RecordResourceUsage(object? state)
    {
        using var meter = new Meter("The-Zach-Zone", "1.0");

        meter.CreateCounter<double>("tzz_resources_mem_used", description: "Used memory")
            .Add(resources.GetUsedRam());

        meter.CreateCounter<double>("tzz_resources_mem_total", description: "Total memory")
            .Add(resources.GetTotalRam());

        meter.CreateCounter<double>("tzz_resources_cpu_used", description: "Used CPU")
            .Add(resources.GetCpuUsage());

        meter.CreateCounter<double>("tzz_resources_disk_used", description: "Used disk space in bytes.")
            .Add(resources.GetUsedDisk());

        meter.CreateCounter<double>("tzz_resources_disk_total", description: "Total disk space in bytes.")
            .Add(resources.GetTotalDisk());
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

}
