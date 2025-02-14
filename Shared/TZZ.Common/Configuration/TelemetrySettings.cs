using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZZ.Common.Configuration;

public class TelemetrySettings
{
    public required string ServiceName { get; set; }
    public required string ServiceVersion { get; set; }
}
