using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class PerformanceTypeInfoRepositoryTest
{
    [Fact]
    public async Task AddPerformanceTypesAsync_AddsPerformanceTypes_ToDatabase()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new PerformanceTypeRepository(context);

        var performanceTypes = new List<PerformanceType>
        {
            new PerformanceType { Id = 1, Name = "Type 1" },
            new PerformanceType { Id = 2, Name = "Type 2" }
        };

        // Act
        await repository.AddPerformanceTypesAsync(performanceTypes);

        // Assert
        var addedTypes = await context.PerformanceTypeInfos.ToListAsync();
        Assert.Equal(2, addedTypes.Count);
        Assert.Contains(addedTypes, pt => pt.Name == "Type 1");
        Assert.Contains(addedTypes, pt => pt.Name == "Type 2");
    }

    [Fact]
    public async Task AddPerformanceTypeAsync_DoesNotAddEmptyList()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new PerformanceTypeRepository(context);

        var performanceTypes = new List<PerformanceType>();

        // Act
        await repository.AddPerformanceTypesAsync(performanceTypes);

        // Assert
        var addedTypes = await context.PerformanceTypeInfos.ToListAsync();
        Assert.Empty(addedTypes);
    }

    [Fact]
    public async Task GetPerformanceTypeInfosAsync_ReturnsAllPerformanceTypes()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new PerformanceTypeRepository(context);

        var performanceTypes = new List<PerformanceType>
        {
            new PerformanceType { Id = 1, Name = "Type 1" },
            new PerformanceType { Id = 2, Name = "Type 2" }
        };

        context.PerformanceTypeInfos.AddRange(performanceTypes);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetPerformanceTypeInfosAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, pt => pt.Name == "Type 1");
        Assert.Contains(result, pt => pt.Name == "Type 2");
    }

    [Fact]
    public async Task GetPerformanceTypeInfosAsync_ReturnsEmptyList_WhenNoPerformanceTypesExist()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new PerformanceTypeRepository(context);

        // Act
        var result = await repository.GetPerformanceTypeInfosAsync();

        // Assert
        Assert.Empty(result);
    }
}