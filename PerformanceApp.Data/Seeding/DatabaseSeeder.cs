using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Seeding.Entities;

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
    private readonly TransactionSeeder _transactionSeeder = new(context);
    private readonly StagingSeeder _stagingSeeder = new(context);
    private readonly TransactionTypeSeeder _transactionTypeSeeder = new(context);
    private readonly UserSeeder _userSeeder = new(userManager);
    private readonly PerformanceTypeSeeder _performanceTypeSeeder = new(context);

    public async Task SeedBaseData()
    {
        await _stagingSeeder.Seed();
        await _dateInfoSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _instrumentSeeder.Seed();
        await _instrumentPriceSeeder.Seed();
    }

    public async Task Seed()
    {
        await SeedBaseData();

        await _userSeeder.Seed();
        await _portfolioSeeder.Seed();
        await _benchmarkSeeder.Seed();
        await _transactionTypeSeeder.Seed();
        await _performanceTypeSeeder.Seed();
        await _transactionSeeder.Seed();
        await _performanceSeeder.Seed();
        await _keyFigureSeeder.Seed();
    }

}