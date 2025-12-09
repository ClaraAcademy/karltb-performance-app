namespace PerformanceApp.Server.Startup.App.Cors;

public static class AppCorsExtensions
{
    public static WebApplication AddCorsServices(this WebApplication app)
    {
        app.UseCors();

        return app;
    }
}