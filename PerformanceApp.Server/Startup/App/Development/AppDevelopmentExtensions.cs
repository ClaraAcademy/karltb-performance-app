using PerformanceApp.Server.Startup.Swagger.Constants;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PerformanceApp.Server.Startup.App.Development;

public static class AppDevelopmentExtensions
{
    public static WebApplication AddDevelopmentServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(AddSwaggerEndpoint);

        return app;
    }

    private static void AddSwaggerEndpoint(SwaggerUIOptions options)
    {
        options.SwaggerEndpoint(SwaggerConstants.Endpoint, SwaggerConstants.Name);
    }


}