using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZZ.Common;

public static class ArdalisExtensions
{
    public static bool False(this IGuardClause clause, bool value, string message = "False was expected.")
    {
        if (!value)
        {
            new ArgumentException(message);
        }
        return value;
    }
}
