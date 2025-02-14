using Hardware.Info;
using System.Diagnostics;

namespace TZZ.Core.Shared.Services;

#pragma warning disable CA1416 // Validate platform compatibility
public class ResourceService
{
    private IHardwareInfo info;
    public ResourceService()
    {
        info = new HardwareInfo();
        info.RefreshCPUList();
        info.RefreshMemoryStatus();
    }

    public ulong GetTotalRam()
    {
        return info.MemoryStatus.TotalPhysical;
    }

    public ulong GetUsedRam()
    {
        return info.MemoryStatus.TotalPhysical - info.MemoryStatus.AvailablePhysical;
    }

    public ulong GetTotalDisk()
    {
        var disk = new DriveInfo("C");
        return (ulong)disk.TotalSize;
    }
    public ulong GetUsedDisk()
    {
        var disk = new DriveInfo("C");
        return (ulong)(disk.TotalSize - disk.AvailableFreeSpace);
    }
    public float GetCpuUsage()
    {
        var memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        return memoryCounter.NextValue();
    }
}

#pragma warning restore CA1416 // Validate platform compatibility