namespace PerformanceApp.Server.App.Security;

public static class AppSecurityExtensions
{
    public static WebApplication AddSecurityServices(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}