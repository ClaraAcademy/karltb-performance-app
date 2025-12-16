using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Builders;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class KeyFigureRepositoryTest : BaseRepositoryTest
{
    private readonly KeyFigureInfoRepository _repository;

    public KeyFigureRepositoryTest()
    {
        _repository = new KeyFigureInfoRepository(_context);
    }

    [Fact]
    public async Task AddKeyFigureInfosAsync_AddsKeyFigures()
    {
        // Arrange
        var expected = new KeyFigureInfoBuilder()
            .Many(5)
            .ToList();

        // Act
        await _repository.AddKeyFigureInfosAsync(expected);

        // Assert
        var actual = await _context.KeyFigureInfos.ToListAsync();

        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task AddKeyFigureInfosAsync_NoKeyFigures_DoesNothing()
    {
        // Arrange
        var empty = new List<KeyFigureInfo>();

        // Act
        await _repository.AddKeyFigureInfosAsync(empty);

        // Assert
        var actual = await _context.KeyFigureInfos.ToListAsync();
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetKeyFigureInfosAsync_ReturnsAllKeyFigures()
    {
        // Arrange
        var expected = new KeyFigureInfoBuilder()
            .Many(8)
            .ToList();

        await _context.KeyFigureInfos.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var retrieved = await _repository.GetKeyFigureInfosAsync();
        var actual = retrieved.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task GetKeyFigureInfosAsync_NoKeyFigures_ReturnsEmptyList()
    {
        // Act
        var actual = await _repository.GetKeyFigureInfosAsync();

        // Assert
        Assert.Empty(actual);
    }



}