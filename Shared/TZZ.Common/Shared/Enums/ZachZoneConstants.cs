namespace TZZ.Common.Shared.Enums;

public static class ZachZoneConstants
{
    public static class Policies
    {
        public static string Default => nameof(Default);
        public static string Admin => nameof(Admin);
    }

    public static class ClaimTypes
    {
        public static string Role => nameof(Role);
    }

    public static class Roles
    {
        public static string User => nameof(User);
        public static string Admin => nameof(Admin);
    }
}
