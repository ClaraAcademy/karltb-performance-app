using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class DateInfoSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    [Fact]
    public async Task Seed_AddsDateInfos()
    {
        // Arrange
        var expected = BankdayData.ExpectedBankdays;

        // Act
        var dateInfos = await _context.DateInfos.ToListAsync();
        var actual = dateInfos.Select(di => di.Bankday).OrderBy(d => d).ToList();

        // Assert
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        var initialCount = await _context.DateInfos.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var finalCount = await _context.DateInfos.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}