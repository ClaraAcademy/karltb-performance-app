using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Builders;

namespace PerformanceApp.Infrastructure.Test.Repositories;

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
        var expected = new StagingBuilder()
            .Many(5)
            .ToList();

        // Act
        await _repository.AddStagingsAsync(expected);

        var actual = await _context.Stagings.ToListAsync();
        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach ((var e, var a) in expected.Zip(actual))
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
        var empty = new List<Staging>();

        // Act
        await _repository.AddStagingsAsync(empty);

        // Assert
        var actual = await _context.Stagings.ToListAsync();
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetStagingsAsync_ReturnsAllStagings()
    {
        // Arrange
        var expected = new StagingBuilder()
            .Many(7)
            .ToList();

        await _context.Stagings.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetStagingsAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach ((var e, var a) in expected.Zip(actual))
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
        var actual = await _repository.GetStagingsAsync();

        // Assert
        Assert.Empty(actual);
    }
}