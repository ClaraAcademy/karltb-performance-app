using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding;

public class BaseSeederTest
{
    protected readonly IServiceScope _scope;
    protected readonly PadbContext _context;
    protected readonly UserManager<ApplicationUser> _userManager;
    private readonly DatabaseFixture _fixture;

    /* SEEDERS */
    protected readonly BenchmarkSeeder _benchmarkSeeder;
    protected readonly DateInfoSeeder _dateInfoSeeder;
    protected readonly InstrumentPriceSeeder _instrumentPriceSeeder;
    protected readonly InstrumentSeeder _instrumentSeeder;
    protected readonly InstrumentPerformanceSeeder _instrumentPerformanceSeeder;
    protected readonly InstrumentTypeSeeder _instrumentTypeSeeder;
    protected readonly KeyFigureInfoSeeder _keyFigureInfoSeeder;
    protected readonly KeyFigureSeeder _keyFigureSeeder;
    protected readonly PortfolioPerformanceSeeder _portfolioPerformanceSeeder;
    protected readonly PortfolioSeeder _portfolioSeeder;
    protected readonly PortfolioValueSeeder _portfolioValueSeeder;
    protected readonly PositionValueSeeder _positionValueSeeder;
    protected readonly TransactionSeeder _transactionSeeder;
    protected readonly PositionSeeder _positionSeeder;
    protected readonly StagingSeeder _stagingSeeder;
    protected readonly TransactionTypeSeeder _transactionTypeSeeder;
    protected readonly UserSeeder _userSeeder;
    protected readonly PerformanceTypeSeeder _performanceTypeSeeder;

    public BaseSeederTest(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture
            .ServiceProvider
            .CreateScope();
        _context = _scope
            .ServiceProvider
            .GetRequiredService<PadbContext>();
        _userManager = _scope
            .ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        /* SEEDERS */
        _stagingSeeder = new(_context);
        _dateInfoSeeder = new(_context);
        _instrumentTypeSeeder = new(_context);
        _instrumentSeeder = new(_context);
        _instrumentPriceSeeder = new(_context);
        _instrumentPerformanceSeeder = new(_context);
        _performanceTypeSeeder = new(_context);
        _keyFigureInfoSeeder = new(_context);
        _keyFigureSeeder = new(_context);
        _portfolioPerformanceSeeder = new(_context);
        _portfolioSeeder = new(_context, _userManager);
        _benchmarkSeeder = new(_context);
        _portfolioValueSeeder = new(_context);
        _positionValueSeeder = new(_context);
        _transactionSeeder = new(_context);
        _positionSeeder = new(_context);
        _transactionTypeSeeder = new(_context);
        _userSeeder = new(_userManager);
    }

    protected async Task Seed()
    {
        await _stagingSeeder.Seed();
        await _dateInfoSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _instrumentSeeder.Seed();
        await _instrumentPriceSeeder.Seed();

        await _transactionTypeSeeder.Seed();
        await _performanceTypeSeeder.Seed();
        await _keyFigureInfoSeeder.Seed();

        await _userSeeder.Seed();
        await _portfolioSeeder.Seed();
        await _benchmarkSeeder.Seed();

        await _transactionSeeder.Seed();
        await _positionSeeder.Seed();
        await _positionValueSeeder.Seed();
        await _portfolioValueSeeder.Seed();

        await _keyFigureSeeder.Seed();
        await _instrumentPerformanceSeeder.Seed();
        await _portfolioPerformanceSeeder.Seed();
    }

}