using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Builders;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class PerformanceTypeInfoRepositoryTest : BaseRepositoryTest
{
    private readonly PerformanceTypeRepository _repository;

    public PerformanceTypeInfoRepositoryTest()
    {
        _repository = new PerformanceTypeRepository(_context);
    }

    [Fact]
    public async Task AddPerformanceTypesAsync_AddsPerformanceTypes_ToDatabase()
    {
        // Arrange
        var expected = new PerformanceTypeBuilder()
            .Many(3)
            .ToList();

        // Act
        await _repository.AddPerformanceTypesAsync(expected);

        // Assert
        var actual = await _context.PerformanceTypeInfos.ToListAsync();

        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task AddPerformanceTypeAsync_DoesNotAddEmptyList()
    {
        // Arrange
        var empty = new List<PerformanceType>();

        // Act
        await _repository.AddPerformanceTypesAsync(empty);
        var actual = await _context
            .PerformanceTypeInfos
            .ToListAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetPerformanceTypeInfosAsync_ReturnsAllPerformanceTypes()
    {
        // Arrange
        var expected = new PerformanceTypeBuilder()
            .Many(7)
            .ToList();

        await _context
            .PerformanceTypeInfos
            .AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetPerformanceTypeInfosAsync();

        // Assert
        Assert.Equal(expected.Count, result.Count);
        foreach (var (e, a) in expected.Zip(result))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task GetPerformanceTypeInfosAsync_ReturnsEmptyList_WhenNoPerformanceTypesExist()
    {
        // Act
        var actual = await _repository.GetPerformanceTypeInfosAsync();

        // Assert
        Assert.Empty(actual);
    }
}