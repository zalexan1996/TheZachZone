app.useEndpoints(endpoints > {
    endpoints.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();

    var nested = endpoints.CreateApplicationBuilder();
    nested.useHealthChecks("/");
    endpoints.Map("/healthz", nested.Build());
})