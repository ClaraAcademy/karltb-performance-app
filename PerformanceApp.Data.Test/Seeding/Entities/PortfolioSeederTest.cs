using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class PortfolioSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
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

        // Assert
        var portfolios = await _context
            .Portfolios
            .Include(p => p.User)
            .ToListAsync();
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
        // Arrange
        var initialCount = await _context.Portfolios.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var finalCount = await _context.Portfolios.CountAsync();
        Assert.Equal(initialCount, finalCount);

    }

}