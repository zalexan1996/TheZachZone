using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace TZZ.WebShared.Health.Checks;

#pragma warning disable CA1416 // Validate platform compatibility
public class ResourceUsageHealthCheck : IHealthCheck
{
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
            {"max_cpu", Constants.Health.PerformanceThresholds.CPU_DEGRADED },
            {"max_mem", Constants.Health.PerformanceThresholds.MEM_DEGRADED },
            {"max_disk", Constants.Health.PerformanceThresholds.DISK_DEGRADED },
        };

        // Unhealthy if any of the metrics are at 95% or higher utilization.
        if (IsUnhealthy(cpu, mem, disk))
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(
                $"System resources are exhausted. CPU: {cpu}%, Memory: {mem}%, Disk: {disk}%.",
                data: data));
        }

        if (IsDegraded(cpu, mem, disk))
        {
            return Task.FromResult(HealthCheckResult.Degraded(
                $"System resources are over the threshold. CPU: {cpu}%, Memory: {mem}%, Disk: {disk}%.",
                data: data));
        }

        return Task.FromResult(HealthCheckResult.Healthy("System resources are within acceptable limits.", data: data));
    }

    private bool IsDegraded(double cpu, double mem, double disk) => cpu > Constants.Health.PerformanceThresholds.CPU_DEGRADED
            || mem > Constants.Health.PerformanceThresholds.MEM_DEGRADED
            || disk > Constants.Health.PerformanceThresholds.DISK_DEGRADED;

    private bool IsUnhealthy(double cpu, double mem, double disk) => cpu > Constants.Health.PerformanceThresholds.CPU_UNHEALTHY
            || mem > Constants.Health.PerformanceThresholds.MEM_UNHEALTHY
            || disk > Constants.Health.PerformanceThresholds.DISK_UNHEALTHY;

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
        return (totalSpace - freeSpace) / totalSpace * 100;
    }
}

#pragma warning restore CA1416 // Validate platform compatibility