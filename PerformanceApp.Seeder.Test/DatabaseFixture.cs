using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Entities;

namespace PerformanceApp.Seeder.Test;

public class DatabaseFixture : IAsyncLifetime
{
    private static readonly string _connectionString
        = "Server=localhost\\SQLEXPRESS;Database=padb_test;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
    protected readonly ServiceProvider _serviceProvider;
    public ServiceProvider ServiceProvider => _serviceProvider;

    public DatabaseFixture()
    {
        _serviceProvider = new ServiceCollection()
            .AddDbContext<PadbContext>(options => options.UseSqlServer(_connectionString))
            .AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<PadbContext>()
            .Services
            .BuildServiceProvider();
    }

    public async Task Seed()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PadbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        /* SEEDERS */
        var stagingSeeder = new StagingSeeder(context);
        var dateInfoSeeder = new DateInfoSeeder(context);
        var instrumentTypeSeeder = new InstrumentTypeSeeder(context);
        var instrumentSeeder = new InstrumentSeeder(context);
        var instrumentPriceSeeder = new InstrumentPriceSeeder(context);
        var instrumentPerformanceSeeder = new InstrumentPerformanceSeeder(context);
        var performanceTypeSeeder = new PerformanceTypeSeeder(context);
        var keyFigureInfoSeeder = new KeyFigureInfoSeeder(context);
        var keyFigureSeeder = new KeyFigureSeeder(context);
        var portfolioPerformanceSeeder = new PortfolioPerformanceSeeder(context);
        var portfolioSeeder = new PortfolioSeeder(context, userManager);
        var benchmarkSeeder = new BenchmarkSeeder(context);
        var portfolioValueSeeder = new PortfolioValueSeeder(context);
        var positionValueSeeder = new PositionValueSeeder(context);
        var transactionSeeder = new TransactionSeeder(context);
        var positionSeeder = new PositionSeeder(context);
        var transactionTypeSeeder = new TransactionTypeSeeder(context);
        var userSeeder = new UserSeeder(userManager);

        await stagingSeeder.Seed();
        await dateInfoSeeder.Seed();
        await instrumentTypeSeeder.Seed();
        await instrumentSeeder.Seed();
        await instrumentPriceSeeder.Seed();

        await transactionTypeSeeder.Seed();
        await performanceTypeSeeder.Seed();
        await keyFigureInfoSeeder.Seed();

        await userSeeder.Seed();
        await portfolioSeeder.Seed();
        await benchmarkSeeder.Seed();

        await transactionSeeder.Seed();
        await positionSeeder.Seed();
        await positionValueSeeder.Seed();
        await portfolioValueSeeder.Seed();

        await instrumentPerformanceSeeder.Seed();
        await portfolioPerformanceSeeder.Seed();
        await keyFigureSeeder.Seed();
    }

    public async Task InitializeAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope
            .ServiceProvider
            .GetRequiredService<PadbContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        await Seed();
    }

    public async Task DisposeAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope
            .ServiceProvider
            .GetRequiredService<PadbContext>();

        await context.Database.EnsureDeletedAsync();
        GC.SuppressFinalize(this);
    }
}