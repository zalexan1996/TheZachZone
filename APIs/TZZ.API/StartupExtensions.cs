using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System.Net;
using TZZ.API.Controllers;
using TZZ.API.Services;
using TZZ.Common.Configuration;
using TZZ.Core.Common.Services;
using TZZ.WebShared;

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

  [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "ASP0014:Suggest using top level route registrations", Justification = "<Pending>")]
  public static void ConfigureTheZachZone(this WebApplication app)
  {
    using var serviceScope = app.Services.CreateScope();
    var appSettings = serviceScope.ServiceProvider.GetService<IOptions<AppSettings>>()?.Value;
    var security = serviceScope.ServiceProvider.GetService<IOptions<AppSecurity>>()?.Value;
    Guard.Against.Null(appSettings, null, "Failed to bind AppSettings from configuration.");
    Guard.Against.Null(security, null, "Failed to bind Security from configuration.");

    var healthcheckDescription = new ApiDescription
    {
      HttpMethod = "GET",
      RelativePath = "/healthcheck",
    };

    healthcheckDescription.SupportedRequestFormats.Add(new ApiRequestFormat
    {
      MediaType = "application/json"
    });

    healthcheckDescription.SupportedResponseTypes.Add(new ApiResponseType
    {
      IsDefaultResponse = true,
      StatusCode = (int)HttpStatusCode.OK,
      ApiResponseFormats = new List<ApiResponseFormat> {
        new ApiResponseFormat
        {
          MediaType = "application/json"
        }
      }
    });

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseOpenTelemetryPrometheusScrapingEndpoint();
    app.UseHttpsRedirection();

    app.UseCors(x =>
    {
      x.AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod()
        .WithOrigins([.. security.AllowedOrigins]);
    });

    app.UseExceptionHandler(_ => { });
    app.UseRouting();
    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
      var nested = endpoints.CreateApplicationBuilder();
      nested.UseHealthChecks("/");
      endpoints.Map("healthz", nested.Build());
    });
  }
}
