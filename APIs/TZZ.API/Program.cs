using Microsoft.AspNetCore.Identity;
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

        var app = builder.Build();

        app.ConfigureTheZachZone();

        app.Run();
    }
}
