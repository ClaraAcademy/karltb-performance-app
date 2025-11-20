using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class KeyFigureRepositoryTest
{
    [Fact]
    public async Task AddKeyFigureInfosAsync_AddsKeyFigures()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new KeyFigureInfoRepository(context);

        var keyFigureInfos = new List<KeyFigureInfo>
        {
            new KeyFigureInfo { Id = 1, Name = "KeyFigure1" },
            new KeyFigureInfo { Id = 2, Name = "KeyFigure2" }
        };

        // Act
        await repository.AddKeyFigureInfosAsync(keyFigureInfos);

        // Assert
        var addedKeyFigures = await context.KeyFigureInfos.ToListAsync();
        Assert.Equal(2, addedKeyFigures.Count);
        Assert.Contains(addedKeyFigures, kf => kf.Name == "KeyFigure1");
        Assert.Contains(addedKeyFigures, kf => kf.Name == "KeyFigure2");

    }

    [Fact]
    public async Task AddKeyFiguresInfosAsync_AddsSingleKeyFigure()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new KeyFigureInfoRepository(context);

        var keyFigureInfos = new List<KeyFigureInfo>
        {
            new KeyFigureInfo { Id = 1, Name = "KeyFigure1" }
        };

        // Act
        await repository.AddKeyFigureInfosAsync(keyFigureInfos);

        // Assert
        var addedKeyFigures = await context.KeyFigureInfos.ToListAsync();
        Assert.Single(addedKeyFigures);
        Assert.Equal("KeyFigure1", addedKeyFigures[0].Name);
    }

    [Fact]
    public async Task AddKeyFigureInfosAsync_NoKeyFigures_DoesNothing()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new KeyFigureInfoRepository(context);

        var keyFigureInfos = new List<KeyFigureInfo>();

        // Act
        await repository.AddKeyFigureInfosAsync(keyFigureInfos);

        // Assert
        var addedKeyFigures = await context.KeyFigureInfos.ToListAsync();
        Assert.Empty(addedKeyFigures);
    }

    [Fact]
    public async Task GetKeyFigureInfosAsync_ReturnsAllKeyFigures()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new KeyFigureInfoRepository(context);

        var keyFigureInfos = new List<KeyFigureInfo>
        {
            new KeyFigureInfo { Id = 1, Name = "KeyFigure1" },
            new KeyFigureInfo { Id = 2, Name = "KeyFigure2" }
        };

        await context.KeyFigureInfos.AddRangeAsync(keyFigureInfos);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetKeyFigureInfosAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, kf => kf.Name == "KeyFigure1");
        Assert.Contains(result, kf => kf.Name == "KeyFigure2");
    }

    [Fact]
    public async Task GetKeyFigureInfosAsync_NoKeyFigures_ReturnsEmptyList()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new KeyFigureInfoRepository(context);

        // Act
        var result = await repository.GetKeyFigureInfosAsync();

        // Assert
        Assert.Empty(result);
    }



}