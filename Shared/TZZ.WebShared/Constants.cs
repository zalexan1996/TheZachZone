namespace TZZ.WebShared;

public static class Constants
{
    public static class Security
    {
        public const string ApplicationDiscriminator = "TheZachZone";
        public const string CookieName = "TheZachZone";
    }

    public static class Health
    {
        public static class Tags
        {
            public const string Services = nameof(Services);
            public const string System = nameof(System);
        }

        public static class PerformanceThresholds
        {
            public const double CPU_UNHEALTHY = 95.0;
            public const double MEM_UNHEALTHY = 95.0;
            public const double DISK_UNHEALTHY = 95.0;

            public const double CPU_DEGRADED = 80.0;
            public const double MEM_DEGRADED = 80.0;
            public const double DISK_DEGRADED = 85.0;
        }
    }

    public static class OTEL
    {
        public const string ServiceName = "The-Zach-Zone";
    }
}
