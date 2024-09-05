using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;
using TZZ.Infrastructure.SQL;
using TZZ.WebShared.Common.Services;
using TZZ.WebShared.Security.Services;
using static TZZ.Common.Shared.Enums.ZachZoneConstants;

namespace TZZ.WebShared;

public static class StartupExtensions
{
    public static void AddWebShared(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddOpenApiDocument();
        services.AddCors();
        services.AddHttpContextAccessor();
        services.AddDataProtection(x => x.ApplicationDiscriminator = "TheZachZone");


        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ZachZoneDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddAuthentication(IdentityConstants.ApplicationScheme);
        services.AddAuthorization(x =>
        {
            x.AddPolicy(Policies.Default, c => c.RequireAuthenticatedUser());

            x.AddPolicy(nameof(Policies.Admin), c => c.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
                .RequireClaim(ClaimTypes.Role, Roles.Admin));
        });
        services.ConfigureApplicationCookie(x =>
        {
            x.SlidingExpiration = true;
            x.Cookie.Name = "TheZachZone";
            x.Cookie.HttpOnly = true;
            x.Cookie.Path = "/";
            x.Cookie.SameSite = SameSiteMode.None;
            x.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            x.Events.OnRedirectToLogin = ctx =>
            {

                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                ctx.Response.ContentType = "application/json";
                return ctx.Response.WriteAsJsonAsync(new
                {
                    error = "Access denied",
                    scheme = ctx.Scheme.Name
                });
            };

            x.Events.OnRedirectToAccessDenied = ctx =>
            {
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                ctx.Response.ContentType = "application/json";
                return ctx.Response.WriteAsJsonAsync(new
                {
                    error = "Access denied"
                });
            };
        });

        services.AddAntiforgery(x =>
        {
            x.HeaderName = "X-CSRF-THEZACHZONE";
            x.FormFieldName = "_AntiForgeryToken";
        });

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IPathLocatorService, PathLocatorService>();

        services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService("The-Zach-Zone"))
                .WithTracing(t => t.AddAspNetCoreInstrumentation().AddSource("The-Zach-Zone"))
                .WithMetrics(m => m.AddAspNetCoreInstrumentation().AddMeter("The-Zach-Zone"));
    }
}
