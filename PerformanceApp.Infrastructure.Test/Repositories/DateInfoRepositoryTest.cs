using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Builders;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Helpers;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class DateInfoRepositoryTest : BaseRepositoryTest
{
    private readonly DateInfoRepository _repository;

    public DateInfoRepositoryTest()
    {
        _repository = new DateInfoRepository(_context);
    }

    [Fact]
    public async Task AddDateInfosAsync_AddsDateInfos()
    {
        // Arrange
        var expected = new DateInfoBuilder()
            .Many(5)
            .ToList();

        // Act
        await _repository.AddDateInfosAsync(expected);
        var actual = await _context
            .DateInfos
            .ToListAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        var orderedExpected = expected.OrderedBankdays();
        var orderedActual = actual.OrderedBankdays();
        Assert.Equal(orderedExpected, orderedActual);
    }

    [Fact]
    public async Task GetDateInfosAsync_ReturnsDateInfos()
    {
        // Arrange
        var expected = new DateInfoBuilder()
            .Many(7)
            .ToList();

        _context.DateInfos.AddRange(expected);
        _context.SaveChanges();

        // Act
        var dateInfos = await _repository.GetDateInfosAsync();
        var actual = dateInfos.ToList();

        Assert.Equal(expected.Count, actual.Count);
        var orderedExpected = expected.OrderedBankdays();
        var orderedActual = actual.OrderedBankdays();
        Assert.Equal(orderedExpected, orderedActual);
    }
}