using PerformanceApp.Server.Startup.App.Constants;

namespace PerformanceApp.Server.Startup.App.Routing;

public static class AppRoutingExtensions
{
    public static WebApplication AddRoutingServices(this WebApplication app)
    {
        app.MapControllers();
        app.MapFallbackToFile(AppConstants.FallbackFile);

        return app;
    }
}