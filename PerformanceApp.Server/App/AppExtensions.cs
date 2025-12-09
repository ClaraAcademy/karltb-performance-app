using PerformanceApp.Server.App.Cors;
using PerformanceApp.Server.App.Routing;
using PerformanceApp.Server.App.Security;

namespace PerformanceApp.Server.App;

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