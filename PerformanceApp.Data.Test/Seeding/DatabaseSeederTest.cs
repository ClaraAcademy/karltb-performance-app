using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Seeding;
using PerformanceApp.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PerformanceApp.Data.Test.Seeding;

public class DatabaseSeederTest : IDisposable
{
    private readonly IServiceScope _scope;
    private readonly PadbContext _context;
    private static IServiceProvider GetServiceProvider()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = config.GetConnectionString("TestContext");

        var services = new ServiceCollection();
        services.AddDbContext<PadbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<PadbContext>();

        return services.BuildServiceProvider();
    }

    public DatabaseSeederTest()
    {
        var services = GetServiceProvider();
        _scope = services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<PadbContext>();
        _context.Database.EnsureDeleted();
        DatabaseInitializer.Initialize(services).GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _scope.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void FullDatabaseSeeding_PopulatesAllTables()
    {
        // Assert
        Assert.NotEmpty(_context.Benchmarks);
        Assert.NotEmpty(_context.DateInfos);
        Assert.NotEmpty(_context.Instruments);
        Assert.NotEmpty(_context.InstrumentPerformances);
        Assert.NotEmpty(_context.InstrumentPrices);
        Assert.NotEmpty(_context.InstrumentTypes);
        Assert.NotEmpty(_context.KeyFigureInfos);
        Assert.NotEmpty(_context.KeyFigureValues);
        Assert.NotEmpty(_context.Portfolios);
        Assert.NotEmpty(_context.PortfolioPerformances);
        Assert.NotEmpty(_context.PortfolioValues);
        Assert.NotEmpty(_context.Positions);
        Assert.NotEmpty(_context.PositionValues);
        Assert.NotEmpty(_context.Stagings);
        Assert.NotEmpty(_context.Transactions);
        Assert.NotEmpty(_context.TransactionTypes);
        Assert.NotEmpty(_context.PerformanceTypeInfos);
    }

    [Fact]
    public async Task FullDatabaseSeeding_IsIdempotent()
    {
        var initialBenchmarksCount = _context.Benchmarks.Count();
        var initialDateInfosCount = _context.DateInfos.Count();
        var initialInstrumentsCount = _context.Instruments.Count();
        var initialInstrumentPerformancesCount = _context.InstrumentPerformances.Count();
        var initialInstrumentPricesCount = _context.InstrumentPrices.Count();
        var initialInstrumentTypesCount = _context.InstrumentTypes.Count();
        var initialKeyFigureInfosCount = _context.KeyFigureInfos.Count();
        var initialKeyFigureValuesCount = _context.KeyFigureValues.Count();
        var initialPortfoliosCount = _context.Portfolios.Count();
        var initialPortfolioPerformancesCount = _context.PortfolioPerformances.Count();
        var initialPortfolioValuesCount = _context.PortfolioValues.Count();
        var initialPositionsCount = _context.Positions.Count();
        var initialPositionValuesCount = _context.PositionValues.Count();
        var initialStagingsCount = _context.Stagings.Count();
        var initialTransactionsCount = _context.Transactions.Count();
        var initialTransactionTypesCount = _context.TransactionTypes.Count();
        var initialPerformanceTypeInfosCount = _context.PerformanceTypeInfos.Count();

        // Re-seed the database
        var services = GetServiceProvider();
        await DatabaseInitializer.Initialize(services);

        // Assert that counts remain the same
        Assert.Equal(initialBenchmarksCount, _context.Benchmarks.Count());
        Assert.Equal(initialDateInfosCount, _context.DateInfos.Count());
        Assert.Equal(initialInstrumentsCount, _context.Instruments.Count());
        Assert.Equal(initialInstrumentPerformancesCount, _context.InstrumentPerformances.Count());
        Assert.Equal(initialInstrumentPricesCount, _context.InstrumentPrices.Count());
        Assert.Equal(initialInstrumentTypesCount, _context.InstrumentTypes.Count());
        Assert.Equal(initialKeyFigureInfosCount, _context.KeyFigureInfos.Count());
        Assert.Equal(initialKeyFigureValuesCount, _context.KeyFigureValues.Count());
        Assert.Equal(initialPortfoliosCount, _context.Portfolios.Count());
        Assert.Equal(initialPortfolioPerformancesCount, _context.PortfolioPerformances.Count());
        Assert.Equal(initialPortfolioValuesCount, _context.PortfolioValues.Count());
        Assert.Equal(initialPositionsCount, _context.Positions.Count());
        Assert.Equal(initialPositionValuesCount, _context.PositionValues.Count());
        Assert.Equal(initialStagingsCount, _context.Stagings.Count());
        Assert.Equal(initialTransactionsCount, _context.Transactions.Count());
        Assert.Equal(initialTransactionTypesCount, _context.TransactionTypes.Count());
        Assert.Equal(initialPerformanceTypeInfosCount, _context.PerformanceTypeInfos.Count());
    }

}