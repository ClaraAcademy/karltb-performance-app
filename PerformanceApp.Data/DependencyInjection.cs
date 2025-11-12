using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace PerformanceApp.Data;

public static class DependencyInjection
{
    private static string GetConnectionString(IConfiguration configuration)
    {
        string message = $"Connection string ${ContextName} not found.";
        return configuration.GetConnectionString(ContextName) 
            ?? throw new InvalidOperationException(message);
    }

    private static void SetupSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PadbContext>(
            options => options.UseSqlServer(GetConnectionString(configuration))
        );

    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IDateInfoRepository, DateInfoRepository>();
        services.AddScoped<IBenchmarkRepository, BenchmarkRepository>();
        services.AddScoped<IPerformanceRepository, PerformanceRepository>();
    }

    private const string ContextName = "PadbContext";
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetupSql(configuration);

        services.AddRepositories();

        return services;
    }
}