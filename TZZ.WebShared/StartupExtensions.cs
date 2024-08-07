﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;
using TZZ.Infrastructure.SQL;
using TZZ.WebShared.Common.Services;
using TZZ.WebShared.Security.Services;

namespace TZZ.WebShared;

public static class StartupExtensions
{
    public static void AddWebShared(this IServiceCollection services, IWebHostEnvironment environment, IConfigurationRoot configuration)
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
            x.AddPolicy("default", c => c.RequireAuthenticatedUser());

            x.AddPolicy("admin", c => c.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme)
                .RequireClaim("Admin"));
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
    }
}
