using PerformanceApp.Server.Startup.App.Cors;
using PerformanceApp.Server.Startup.App.Routing;
using PerformanceApp.Server.Startup.App.Security;

namespace PerformanceApp.Server.Startup.App;

public static class AppExtensions
{
    public static WebApplication AddServices(this WebApplication app)
    {
        app.AddCorsServices();
        app.AddSecurityServices();
        app.AddRoutingServices();

        return app;
    }

}