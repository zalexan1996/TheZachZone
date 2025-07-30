namespace TZZ.WebShared;

public static class SecurityConstants
{
  public const string ApplicationDiscriminator = "TheZachZone";
  public const string CookieName = "TheZachZone";
}
public static class TagConstants
{
  public const string Services = nameof(Services);
  public const string System = nameof(System);
}
public static class PerformanceThresholdConstants
{
  public const double CpuUnhealthy = 95.0;
  public const double MemUnhealthy = 95.0;
  public const double DiskUnhealthy = 95.0;

  public const double CpuDegraded = 80.0;
  public const double MemDegraded = 80.0;
  public const double DiskDegraded = 85.0;
}
public static class OtelConstants
{
  public const string ServiceName = "The-Zach-Zone";
}
