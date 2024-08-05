using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZZ.Common.Configuration;

namespace TZZ.Common;

public static class StartupExtensions
{
    public static void AddCommon(this IServiceCollection services, IConfigurationBuilder configuration)
    {

        TheZachZoneConfigurationBuilder.BuildConfiguration(configuration);
    }
}
