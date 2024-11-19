using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZZ.WebShared.Health;

#pragma warning disable CA1416 // Validate platform compatibility
public class ResourceUsageHealthCheck : IHealthCheck
{
    private readonly double _maxCpuUsage = 80.0;
    private readonly double _maxMemoryUsage = 80.0;
    private readonly double _maxDiskUsage = 85.0;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var cpu = GetCpuUsage();
        var mem = GetMemoryUsage();
        var disk = GetDiskUsage();

        var data = new Dictionary<string, object>
        {
            { "cpu", cpu },
            { "mem", mem },
            { "disk", disk },
            {"max_cpu", _maxCpuUsage },
            {"max_mem", _maxMemoryUsage },
            {"max_disk", _maxDiskUsage },
        };

        // Unhealthy if any of the metrics are at 95% or higher utilization.
        if (100 - cpu <= 5 || 100 - mem <= 5 || 100 - disk <= 5)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(
                $"System resources are exhausted. CPU: {cpu}%, Memory: {mem}%, Disk: {disk}%.",
                data: data));
        }

        if (cpu > _maxCpuUsage || mem > _maxMemoryUsage || disk > _maxDiskUsage)
        {
            return Task.FromResult(HealthCheckResult.Degraded(
                $"System resources are over the threshold. CPU: {cpu}%, Memory: {mem}%, Disk: {disk}%.", 
                data: data));
        }

        return Task.FromResult(HealthCheckResult.Healthy("System resources are within acceptable limits.", data: data));
    }

    private double GetCpuUsage()
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        return cpuCounter.NextValue();
    }

    private double GetMemoryUsage()
    {
        var memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        return memoryCounter.NextValue();
    }

    private double GetDiskUsage()
    {
        var drive = new DriveInfo("C");
        double totalSpace = drive.TotalSize;
        double freeSpace = drive.AvailableFreeSpace;
        return ((totalSpace - freeSpace) / totalSpace) * 100;
    }
}

#pragma warning restore CA1416 // Validate platform compatibility