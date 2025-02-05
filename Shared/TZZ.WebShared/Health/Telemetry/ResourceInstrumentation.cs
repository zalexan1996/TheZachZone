using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Core.Shared.Services;

namespace TZZ.WebShared.Health.Telemetry;

public class ResourceInstrumentation(ITelemetryService<ResourceInstrumentation> t)
{
    private void Test()
    {
        var a = t.StartActivity("Test");

    }
}
