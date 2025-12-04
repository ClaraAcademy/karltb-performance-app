using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection("Seeding collection")]
public class StagingSeederTest : BaseSeederTest
{
    private readonly StagingSeeder _stagingSeeder;
    public StagingSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _stagingSeeder = new StagingSeeder(_context);
    }

    [Fact]
    public async Task Seed_AddsStagingData_WhenDatabaseIsEmpty()
    {
        // Act
        await _stagingSeeder.Seed();

        // Assert
        var stagingDataCount = await _context.Stagings.CountAsync();
        Assert.True(stagingDataCount > 0);
    }
}