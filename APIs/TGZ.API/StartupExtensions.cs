using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using TGZ.API.Controllers;
using TGZ.API.Services;
using TZZ.Common.Configuration;
using TZZ.Core.Shared.Services;
using TZZ.Infrastructure.SQL;
using TZZ.WebShared;

namespace TGZ.API;

public static class StartupExtensions
{
    public static void AddTheGameZone(this IServiceCollection services, IWebHostEnvironment environment, IConfigurationRoot configuration)
    {
        services.AddWebShared(environment, configuration);
        services.AddControllers()
            .AddApplicationPart(typeof(GameController).Assembly);
        services.AddTransient<ICurrentUserService, CurrentUserService>();
    }

    public static void ConfigureTheGameZone(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var appSettings = serviceScope.ServiceProvider.GetService<IOptions<AppSettings>>()?.Value;
        var security = serviceScope.ServiceProvider.GetService<IOptions<Security>>()?.Value;
        Guard.Against.Null(appSettings, null, "Failed to bind AppSettings from configuration.");
        Guard.Against.Null(security, null, "Failed to bind Security from configuration.");

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
                .WithOrigins(security.AllowedOrigins);
        });

        app.UseStaticFiles(appSettings.StaticFilesFolderName);
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
