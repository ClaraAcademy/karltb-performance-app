using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data;

public static class DataServices
{
    private static void SetupIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<PadbContext>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IDateInfoRepository, DateInfoRepository>();
        services.AddScoped<IBenchmarkRepository, BenchmarkRepository>();
        services.AddScoped<IKeyFigureValueRepository, KeyFigureValueRepository>();
        services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
        services.AddScoped<IKeyFigureInfoRepository, KeyFigureInfoRepository>();
        services.AddScoped<IStagingRepository, StagingRepository>();
        services.AddScoped<IInstrumentTypeRepository, InstrumentTypeRepository>();
        services.AddScoped<IInstrumentRepository, InstrumentRepository>();
        services.AddScoped<IInstrumentPriceRepository, InstrumentPriceRepository>();
        services.AddScoped<IPerformanceTypeRepository, PerformanceTypeRepository>();
        services.AddScoped<IPortfolioValueRepository, PortfolioValueRepository>();
        services.AddScoped<IInstrumentPerformanceRepository, InstrumentPerformanceRepository>();
        services.AddScoped<IPortfolioPerformanceRepository, PortfolioPerformanceRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IPositionValueRepository, PositionValueRepository>();
    }

    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PadbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException($"Connection string PadbContext not found")
            )
        );

        services.AddRepositories();

        services.SetupIdentity();

        return services;
    }
}