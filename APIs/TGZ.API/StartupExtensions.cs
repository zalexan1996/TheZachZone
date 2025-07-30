using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using TGZ.API.Controllers;
using TGZ.API.Services;
using TZZ.Common.Configuration;
using TZZ.Core.Common.Services;
using TZZ.WebShared;

namespace TGZ.API;

public static class StartupExtensions
{
  public static void AddTheGameZone(this IServiceCollection services, IWebHostEnvironment environment, ConfigurationManager configuration)
  {
    var assemblyLocation = typeof(GameController).Assembly.Location;
    var appSettingsJson = Path.Combine(Path.GetDirectoryName(assemblyLocation)!, "appsettings.tgz.json");
    configuration.AddJsonFile(appSettingsJson, optional: false);
    services.AddWebShared(configuration);
    services.AddControllers()
      .AddApplicationPart(typeof(GameController).Assembly);
    services.AddTransient<ICurrentUserService, CurrentUserService>();
  }

  public static void ConfigureTheGameZone(this WebApplication app)
  {
    using var serviceScope = app.Services.CreateScope();
    var appSettings = serviceScope.ServiceProvider.GetService<IOptions<AppSettings>>()?.Value;
    var security = serviceScope.ServiceProvider.GetService<IOptions<AppSecurity>>()?.Value;
    Guard.Against.Null(appSettings, null, "Failed to bind AppSettings from configuration.");
    Guard.Against.Null(security, null, "Failed to bind Security from configuration.");

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }


    app.UseOpenTelemetryPrometheusScrapingEndpoint();
    app.UseHttpsRedirection();

    app.Use(async (ctx, next) =>
    {
      ctx.Response?.Headers?.Append("Cross-Origin-Opener-Policy", "same-origin");
      ctx.Response?.Headers?.Append("Cross-Origin-Embedder-Policy", "require-corp");
      ctx.Response?.Headers?.Append("Cross-Origin-Resource-Policy", "cross-origin");
      ctx.Response?.Headers?.Append("Content-Security-Policy", "frame-ancestors 'self' http://localhost:8010");
      await next.Invoke();
    });

    app.UseCors(x =>
    {
      x.AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod()
        .WithHeaders("Cross-Origin-Opener-Policy", "Cross-Origin-Embedder-Policy", "Cross-Origin-Resource-Policy", "Content-Type")
        .WithOrigins([.. security.AllowedOrigins]);
    });

    app.UseStaticFiles(new StaticFileOptions()
    {
      ServeUnknownFileTypes = true,
    });

    app.UseExceptionHandler(_ => { });
    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization();

  }
}
