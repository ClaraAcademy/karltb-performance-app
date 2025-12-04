using DocumentFormat.OpenXml.Office2021.Excel.RichDataWebImage;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class DateInfoSeederTest : BaseSeederTest
{
    private readonly StagingSeeder _stagingSeeder;
    private readonly DateInfoSeeder _dateInfoSeeder;

    public DateInfoSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _stagingSeeder = new StagingSeeder(_context);
        _dateInfoSeeder = new DateInfoSeeder(_context);
    }

    [Fact]
    public async Task Seed_AddsDateInfos()
    {
        // Act
        var expected = BankdayData.ExpectedBankdays;

        await _stagingSeeder.Seed();
        await _dateInfoSeeder.Seed();

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
        await _stagingSeeder.Seed();
        await _dateInfoSeeder.Seed();
        var initialCount = await _context.DateInfos.CountAsync();

        // Act
        await _dateInfoSeeder.Seed();

        // Assert
        var finalCount = await _context.DateInfos.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}