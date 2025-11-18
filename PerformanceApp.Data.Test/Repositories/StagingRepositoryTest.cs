using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class StagingRepositoryTest
{
    [Fact]
    public async Task AddStagingsAsync_AddsStagingsToDatabase()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new StagingRepository(context);

        var bankday = DateOnly.FromDateTime(DateTime.Now);

        var stagings = new List<Staging>
        {
            new Staging {Bankday = bankday, InstrumentType = "Type1", InstrumentName = "Staging1", Price = 100.0m, Created = DateTime.Now },
            new Staging {Bankday = bankday, InstrumentType = "Type2", InstrumentName = "Staging2", Price = 200.0m, Created = DateTime.Now }
        };

        // Act
        await repository.AddStagingsAsync(stagings);

        // Assert
        var addedStagings = await context.Stagings.ToListAsync();
        Assert.Equal(2, addedStagings.Count);
        Assert.Contains(addedStagings, s => s.InstrumentName == "Staging1");
        Assert.Contains(addedStagings, s => s.InstrumentName == "Staging2");
    }

    [Fact]
    public async Task AddStagingsAsync_EmptyList_DoesNotAddAnything()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new StagingRepository(context);

        var stagings = new List<Staging>();

        // Act
        await repository.AddStagingsAsync(stagings);

        // Assert
        var addedStagings = await context.Stagings.ToListAsync();
        Assert.Empty(addedStagings);
    }

    [Fact]
    public async Task GetStagingsAsync_ReturnsAllStagings()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new StagingRepository(context);

        var bankday = DateOnly.FromDateTime(DateTime.Now);

        var stagings = new List<Staging>
        {
            new Staging {Bankday = bankday, InstrumentType = "Type1", InstrumentName = "Staging1", Price = 100.0m, Created = DateTime.Now },
            new Staging {Bankday = bankday, InstrumentType = "Type2", InstrumentName = "Staging2", Price = 200.0m, Created = DateTime.Now }
        };

        await context.Stagings.AddRangeAsync(stagings);
        await context.SaveChangesAsync();

        // Act
        var retrievedStagings = await repository.GetStagingsAsync();

        // Assert
        Assert.Equal(2, retrievedStagings.Count);
        Assert.Contains(retrievedStagings, s => s.InstrumentName == "Staging1");
        Assert.Contains(retrievedStagings, s => s.InstrumentName == "Staging2");
    }

    [Fact]
    public async Task GetStagingsAsync_NoStagings_ReturnsEmptyList()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new StagingRepository(context);

        // Act
        var retrievedStagings = await repository.GetStagingsAsync();

        // Assert
        Assert.Empty(retrievedStagings);
    }
}