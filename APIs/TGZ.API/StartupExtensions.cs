using TGZ.API.Controllers;
using TGZ.API.Services;
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
                .WithOrigins(["http://localhost:8000", "http://localhost:8010", "http://thezachzone.dryrlent.ddns.net", "http://thegamezone.dryrlent.ddns.ent"]);
        });

        app.UseStaticFiles();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
