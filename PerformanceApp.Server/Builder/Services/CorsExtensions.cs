using Microsoft.AspNetCore.Cors.Infrastructure;

namespace PerformanceApp.Server.Builder.Services;

public static class CorsExtensions
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(AddCorsPolicy);
        return services;
    }

    static void AddCorsPolicy(CorsOptions options)
    {
        options.AddDefaultPolicy(AddCorsPolicyBuilder);
    }

    static void AddCorsPolicyBuilder(CorsPolicyBuilder policyBuilder)
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    }
}