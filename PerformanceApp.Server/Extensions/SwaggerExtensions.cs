using PerformanceApp.Server.Swagger.Requirements;
using PerformanceApp.Server.Swagger.Schemes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PerformanceApp.Server.Extensions;

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
    }

}