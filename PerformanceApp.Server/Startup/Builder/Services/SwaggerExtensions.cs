using PerformanceApp.Server.Startup.Swagger.Documentation;
using PerformanceApp.Server.Startup.Swagger.Requirements;
using PerformanceApp.Server.Startup.Swagger.Schemes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PerformanceApp.Server.Startup.Builder.Services;

public static class SwaggerExtensions
{
    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(AddSwaggerGen);

        return services;
    }

    static void AddSwaggerGen(SwaggerGenOptions c)
    {
        c.AddSecurityDefinition("Bearer", BearerSecurityProvider.Scheme);
        c.AddSecurityRequirement(BearerSecurityRequirement.Requirement);
        c.SwaggerDoc(Documentation.Info.Version, Documentation.Info);
    }

}