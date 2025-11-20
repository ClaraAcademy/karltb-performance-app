using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class KeyFigureRepositoryTest : BaseRepositoryTest
{
    private readonly KeyFigureInfoRepository _repository;

    public KeyFigureRepositoryTest()
    {
        _repository = new KeyFigureInfoRepository(_context);
    }

    private static string GetName(int i) => $"KeyFigure{i}";

    private static List<KeyFigureInfo> CreateKeyFigureInfos(int count)
    {
        return Enumerable.Range(1, count)
            .Select(i => new KeyFigureInfo { Id = i, Name = GetName(i) })
            .ToList();
    }

    [Fact]
    public async Task AddKeyFigureInfosAsync_AddsKeyFigures()
    {
        int n = 3;
        var keyFigureInfos = CreateKeyFigureInfos(n);

        // Act
        await _repository.AddKeyFigureInfosAsync(keyFigureInfos);

        // Assert
        var addedKeyFigures = await _context.KeyFigureInfos.ToListAsync();

        Assert.Equal(n, addedKeyFigures.Count);
        for (int i = 1; i <= n; i++)
        {
            Assert.Contains(addedKeyFigures, kf => kf.Name == GetName(i));
        }
    }

    [Fact]
    public async Task AddKeyFiguresInfosAsync_AddsSingleKeyFigure()
    {
        // Arrange
        int n = 1;
        var expected = CreateKeyFigureInfos(n);

        // Act
        await _repository.AddKeyFigureInfosAsync(expected);

        // Assert
        var actual = await _context.KeyFigureInfos.ToListAsync();
        Assert.Single(actual);
        Assert.Equal("KeyFigure1", actual[0].Name);
    }

    [Fact]
    public async Task AddKeyFigureInfosAsync_NoKeyFigures_DoesNothing()
    {
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
        var n = 2;
        var expected = CreateKeyFigureInfos(n);

        await _context.KeyFigureInfos.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetKeyFigureInfosAsync();

        // Assert
        Assert.Equal(n, actual.Count());
        for (int i = 1; i <= n; i++)
        {
            Assert.Contains(actual, kf => kf.Name == GetName(i));
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