using Hardware.Info;
using System.Diagnostics;

namespace TZZ.Core.Common.Services;

#pragma warning disable CA1416 // Validate platform compatibility
public class ResourceService
{
  private HardwareInfo info;
  public ResourceService()
  {
    info = new HardwareInfo();
    info.RefreshCPUList();
    info.RefreshMemoryStatus();
  }

  public ulong TotalRam => info.MemoryStatus.TotalPhysical;

  public ulong UsedRam() => info.MemoryStatus.TotalPhysical - info.MemoryStatus.AvailablePhysical;

  public static ulong TotalDisk => (ulong)new DriveInfo("C").TotalSize;
  public static ulong UsedDisk
  {
    get
    {
      var disk = new DriveInfo("C");
      return (ulong)(disk.TotalSize - disk.AvailableFreeSpace);
    }
  }
  public static float CpuUsage
  {
    get
    {
      using var memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
      return memoryCounter.NextValue();
    }
  }
}

#pragma warning restore CA1416 // Validate platform compatibility