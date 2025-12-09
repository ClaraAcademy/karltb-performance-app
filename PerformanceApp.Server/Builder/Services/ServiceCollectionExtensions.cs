using PerformanceApp.Server.Services;
using PerformanceApp.Data;

namespace PerformanceApp.Server.Builder.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataServices(configuration);

        services.AddScoped<IPortfolioService, PortfolioService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IDateInfoService, DateInfoService>();
        services.AddScoped<ISvgService, SvgService>();
        services.AddScoped<IPerformanceService, PerformanceService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}