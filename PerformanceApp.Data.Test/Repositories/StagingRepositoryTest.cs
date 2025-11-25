using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class StagingRepositoryTest : BaseRepositoryTest
{
    private readonly StagingRepository _repository;

    public StagingRepositoryTest()
    {
        _repository = new StagingRepository(_context);
    }

    private static Staging CreateStaging(int i)
    {
        return new Staging
        {
            Bankday = DateOnly.FromDateTime(DateTime.Now),
            InstrumentType = $"Type{i}",
            InstrumentName = $"Staging{i}",
            Price = 100.0m * i,
            Created = DateTime.Now
        };
    }

    private static List<Staging> CreateStagings(int count)
    {
        return Enumerable.Range(1, count)
            .Select(i => CreateStaging(i))
            .ToList();
    }

    [Fact]
    public async Task AddStagingsAsync_AddsStagingsToDatabase()
    {
        // Arrange
        var nExpected = 5;
        var stagings = CreateStagings(nExpected);

        // Act
        await _repository.AddStagingsAsync(stagings);

        // Assert
        var addedStagings = await _context.Stagings.ToListAsync();

        var nActual = addedStagings.Count;
        Assert.Equal(nExpected, nActual);

        foreach ((var e, var a) in stagings.Zip(addedStagings))
        {
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(e.InstrumentType, a.InstrumentType);
            Assert.Equal(e.InstrumentName, a.InstrumentName);
            Assert.Equal(e.Price, a.Price);
        }
    }

    [Fact]
    public async Task AddStagingsAsync_EmptyList_DoesNotAddAnything()
    {
        // Arrange
        var stagings = new List<Staging>();

        // Act
        await _repository.AddStagingsAsync(stagings);

        // Assert
        var addedStagings = await _context.Stagings.ToListAsync();
        Assert.Empty(addedStagings);
    }

    [Fact]
    public async Task GetStagingsAsync_ReturnsAllStagings()
    {
        // Arrange
        var nExpected = 8;
        var stagings = CreateStagings(nExpected);

        await _context.Stagings.AddRangeAsync(stagings);
        await _context.SaveChangesAsync();

        // Act
        var retrievedStagings = await _repository.GetStagingsAsync();

        // Assert
        var nActual = retrievedStagings.Count;
        Assert.Equal(nExpected, nActual);
        foreach ((var e, var a) in stagings.Zip(retrievedStagings))
        {
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(e.InstrumentType, a.InstrumentType);
            Assert.Equal(e.InstrumentName, a.InstrumentName);
            Assert.Equal(e.Price, a.Price);
        }
    }

    [Fact]
    public async Task GetStagingsAsync_NoStagings_ReturnsEmptyList()
    {
        // Act
        var retrievedStagings = await _repository.GetStagingsAsync();

        // Assert
        Assert.Empty(retrievedStagings);
    }
}