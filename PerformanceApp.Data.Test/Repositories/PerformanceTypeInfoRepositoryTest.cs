using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class PerformanceTypeInfoRepositoryTest : BaseRepositoryTest
{
    private readonly PerformanceTypeRepository _repository;

    public PerformanceTypeInfoRepositoryTest()
    {
        _repository = new PerformanceTypeRepository(_context);
    }

    private static string CreateName(int i) => $"PerformanceType{i}";
    private static PerformanceType CreatePerformanceType(int i) => new() { Id = i, Name = CreateName(i) };
    private static List<PerformanceType> CreatePerformanceTypes(int count)
    {
        return Enumerable.Range(1, count)
            .Select(CreatePerformanceType)
            .ToList();
    }

    [Fact]
    public async Task AddPerformanceTypesAsync_AddsPerformanceTypes_ToDatabase()
    {
        // Arrange
        var n = 2;
        var performanceTypes = CreatePerformanceTypes(n);

        // Act
        await _repository.AddPerformanceTypesAsync(performanceTypes);

        // Assert
        var addedTypes = await _context.PerformanceTypeInfos.ToListAsync();
        Assert.Equal(n, addedTypes.Count);
        for (int i = 1; i <= n; i++)
        {
            Assert.Contains(addedTypes, pt => pt.Name == CreateName(i));
        }
    }

    [Fact]
    public async Task AddPerformanceTypeAsync_DoesNotAddEmptyList()
    {
        // Arrange
        var expected = new List<PerformanceType>();

        // Act
        await _repository.AddPerformanceTypesAsync(expected);

        // Assert
        var actual = await _context.PerformanceTypeInfos.ToListAsync();
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetPerformanceTypeInfosAsync_ReturnsAllPerformanceTypes()
    {
        // Arrange
        var n = 100;
        var performanceTypes = CreatePerformanceTypes(n);

        _context.PerformanceTypeInfos.AddRange(performanceTypes);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetPerformanceTypeInfosAsync();

        // Assert
        Assert.Equal(n, result.Count());
        for (int i = 1; i <= n; i++)
        {
            Assert.Contains(result, pt => pt.Name == CreateName(i));
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