using Microsoft.IdentityModel.Tokens;
using TZZ.API.Controllers;
using TZZ.Infrastructure.SQL;
using TZZ.WebShared;
using TZZ.Common;
using TZZ.Core.Shared.Services;
using TZZ.API.Services;
using TZZ.Common.Configuration;

namespace TZZ.API;

public static class StartupExtensions
{
    public static void AddTheZachZone(this IServiceCollection services, IWebHostEnvironment environment, ConfigurationManager configuration)
    {
        var assemblyLocation = typeof(AccountController).Assembly.Location;
        var appSettingsJson = Path.Combine(Path.GetDirectoryName(assemblyLocation)!, "appsettings.tzz.json");
        configuration.AddJsonFile(appSettingsJson, optional: false);

        services.AddWebShared(configuration);
        services.AddControllers().AddApplicationPart(typeof(AccountController).Assembly);
        services.AddTransient<ICurrentUserService, CurrentUserService>();
    }

    public static void ConfigureTheZachZone(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(x => {
            x.AllowAnyHeader()
            .AllowCredentials()
                .AllowAnyMethod()
                .WithOrigins(["http://localhost:8000", "http://localhost:8010", 
                    "http://thezachzone.dryrlent.ddns.net:8000", 
                    "http://thegamezone.dryrlent.ddns.net:8010"]);
        });

        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
