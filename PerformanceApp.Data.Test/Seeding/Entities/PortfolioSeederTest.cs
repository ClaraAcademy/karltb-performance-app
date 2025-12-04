using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class PortfolioSeederTest : BaseSeederTest
{
    private readonly PortfolioSeeder _portfolioSeeder;
    private readonly UserSeeder _userSeeder;
    public PortfolioSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _userSeeder = new UserSeeder(_userManager);
        _portfolioSeeder = new PortfolioSeeder(_context, _userManager);
    }

    private static void AssertPortfolioExists(IEnumerable<Portfolio> portfolios, string portfolioName, string username)
    {
        var portfolio = portfolios.FirstOrDefault(p => p.Name == portfolioName);
        Assert.NotNull(portfolio);
        Assert.NotNull(portfolio.User);
        Assert.Equal(portfolio.User.UserName, username);
    }

    [Fact]
    public async Task Seed_AddsPortfolios_WhenDatabaseIsEmpty()
    {
        // Arrange
        var portfolioA = PortfolioData.PortfolioA;
        var benchmarkA = PortfolioData.BenchmarkA;

        var portfolioB = PortfolioData.PortfolioB;
        var benchmarkB = PortfolioData.BenchmarkB;

        var userA = UserData.UsernameA;
        var userB = UserData.UsernameB;

        var mappings = new List<(string PortfolioName, string Username)>
        {
            (portfolioA, userA),
            (benchmarkA, userA),
            (portfolioB, userB),
            (benchmarkB, userB)
        };

        // Act
        await _userSeeder.Seed();
        await _portfolioSeeder.Seed();

        // Assert
        var portfolios = await _context.Portfolios.ToListAsync();
        Assert.NotEmpty(portfolios);
        Assert.Equal(mappings.Count, portfolios.Count);

        foreach (var (portfolioName, username) in mappings)
        {
            AssertPortfolioExists(portfolios, portfolioName, username);
        }
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange & Act
        await Seed_AddsPortfolios_WhenDatabaseIsEmpty();

        await _portfolioSeeder.Seed();

        // Assert
        var portfolios = await _context.Portfolios.ToListAsync();
        Assert.NotEmpty(portfolios);
        Assert.Equal(4, portfolios.Count);

    }

}