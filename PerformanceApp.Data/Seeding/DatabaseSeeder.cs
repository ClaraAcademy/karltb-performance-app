using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Seeding;

public class DatabaseSeeder(PadbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly BenchmarkSeeder _benchmarkSeeder = new(context);
    private readonly DateInfoSeeder _dateInfoSeeder = new(context);
    private readonly InstrumentPriceSeeder _instrumentPriceSeeder = new(context);
    private readonly InstrumentSeeder _instrumentSeeder = new(context);
    private readonly InstrumentTypeSeeder _instrumentTypeSeeder = new(context);
    private readonly KeyFigureSeeder _keyFigureSeeder = new(context);
    private readonly PerformanceSeeder _performanceSeeder = new(context);
    private readonly PortfolioSeeder _portfolioSeeder = new(context, userManager);
    private readonly PositionSeeder _positionSeeder = new(context);
    private readonly StagingSeeder _stagingSeeder = new(context);
    private readonly TransactionTypeSeeder _transactionTypeSeeder = new(context);
    private readonly UserSeeder _userSeeder = new(userManager);

    public async Task SeedBaseData()
    {
        _stagingSeeder.Seed();
        await _dateInfoSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _instrumentSeeder.Seed();
        _instrumentPriceSeeder.Seed();
    }

    public async Task Seed()
    {
        await SeedBaseData();

        _userSeeder.Seed();
        _portfolioSeeder.Seed();
        await _benchmarkSeeder.Seed();
        _transactionTypeSeeder.Seed();
        await _keyFigureSeeder.Seed();
        _positionSeeder.Seed();
        _performanceSeeder.Seed();
    }

}