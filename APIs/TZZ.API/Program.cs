using Microsoft.AspNetCore.Identity;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using TZZ.Common;
using TZZ.Core;
using TZZ.Infrastructure;

namespace TZZ.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCommon(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddCore(builder.Configuration);
        builder.Services.AddTheZachZone(builder.Environment, builder.Configuration);

        builder.Logging.AddOpenTelemetry(o =>
        {
            o.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("The-Zach-Zone"))
                .AddConsoleExporter();
        });

        var app = builder.Build();

        app.ConfigureTheZachZone();

        app.Run();
    }
}
