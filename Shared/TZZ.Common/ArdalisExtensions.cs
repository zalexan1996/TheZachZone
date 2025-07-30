using Ardalis.GuardClauses;

namespace TZZ.Common;

public static class ArdalisExtensions
{
  public static bool False(this IGuardClause clause, bool value, string message = "False was expected.")
  {
    if (!value)
    {
      throw new ArgumentException(message);
    }
    return value;
  }
}
