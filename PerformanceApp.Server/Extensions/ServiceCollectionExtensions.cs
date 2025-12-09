using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using PerformanceApp.Data;
using PerformanceApp.Data.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;

namespace PerformanceApp.Server.Extensions;

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