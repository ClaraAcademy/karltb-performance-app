using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class BenchmarkRepositoryTest : BaseRepositoryTest
{
    private readonly BenchmarkRepository _repository;

    public BenchmarkRepositoryTest()
    {
        _repository = new BenchmarkRepository(_context);
    }

    [Fact]
    public async Task AddBenchmarkMappingsAsync_AddsBenchmarksToDatabase()
    {
        // Arrange
        var expected = new BenchmarkBuilder()
            .Many(3)
            .ToList();

        // Act
        await _repository.AddBenchmarkMappingsAsync(expected);

        var actual = _context
            .Benchmarks
            .ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            var e = expected[i];
            var a = actual[i];

            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.BenchmarkId, a.BenchmarkId);
        }
    }

    [Fact]
    public async Task GetBenchmarkMappingsAsync_ReturnsAllBenchmarks()
    {
        // Arrange
        var expected = new BenchmarkBuilder()
            .Many(5)
            .ToList();

        await _context
            .Benchmarks
            .AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetBenchmarkMappingsAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count());
        for (int i = 0; i < expected.Count; i++)
        {
            var e = expected[i];
            var a = actual.ElementAt(i);

            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.BenchmarkId, a.BenchmarkId);
        }
    }
}