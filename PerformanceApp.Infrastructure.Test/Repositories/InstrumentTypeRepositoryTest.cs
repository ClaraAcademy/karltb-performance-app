using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Builders;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class InstrumentTypeRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentTypeRepository _repository;

    public InstrumentTypeRepositoryTest()
    {
        _repository = new InstrumentTypeRepository(_context);
    }

    [Fact]
    public async Task AddInstrumentTypesAsync_AddsMultipleInstrumentTypesToDatabase()
    {
        // Arrange
        var expected = new InstrumentTypeBuilder()
            .Many(4)
            .ToList();

        // Act
        await _repository.AddInstrumentTypesAsync(expected);
        var actual = await _context.InstrumentTypes.ToListAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count);

        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task AddInstrumentTypesAsync_DoesNotAddEmptyList()
    {
        // Arrange
        var empty = new List<InstrumentType>();

        // Act
        await _repository.AddInstrumentTypesAsync(empty);
        var actual = await _context.InstrumentTypes.ToListAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetInstrumentTypesAsync_ReturnsCorrectInstrumentTypes()
    {
        // Arrange
        var stock = new InstrumentTypeBuilder()
            .WithName("Stock")
            .Build();
        var bond = new InstrumentTypeBuilder()
            .WithName("Bond")
            .Build();
        var index = new InstrumentTypeBuilder()
            .WithName("Index")
            .Build();

        _context.InstrumentTypes.AddRange([ stock, bond, index ]);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetInstrumentTypesAsync(["Stock", "Bond"]);

        // Assert
        Assert.Equal(2, actual.Count);
        Assert.Contains(actual, it => it.Name == "Stock");
        Assert.Contains(actual, it => it.Name == "Bond");
    }
}