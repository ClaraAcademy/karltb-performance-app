using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class BenchmarkRepositoryTest : BaseRepositoryTest
{
    private readonly BenchmarkRepository _repository;

    private static List<Benchmark> CreateBenchmarks()
    {
        return [
            new Benchmark { PortfolioId = 1, BenchmarkId = 3 },
            new Benchmark { PortfolioId = 2, BenchmarkId = 4 }
        ];
    }

    private static List<Portfolio> CreatePortfolios()
    {
        return [
            new Portfolio { Id = 1, Name = "Portfolio 1" },
            new Portfolio { Id = 2, Name = "Portfolio 2" },
            new Portfolio { Id = 3, Name = "Portfolio 3" },
            new Portfolio { Id = 4, Name = "Portfolio 4" }
        ];
    }

    public BenchmarkRepositoryTest()
    {
        _repository = new BenchmarkRepository(_context);
    }

    [Fact]
    public async Task AddBenchmarkMappingsAsync_AddsBenchmarksToDatabase()
    {
        var benchmarks = CreateBenchmarks();

        await _repository.AddBenchmarkMappingsAsync(benchmarks);

        var retrievedBenchmark1 = await _context.Benchmarks.FindAsync(1, 3);
        var retrievedBenchmark2 = await _context.Benchmarks.FindAsync(2, 4);

        Assert.NotNull(retrievedBenchmark1);
        Assert.NotNull(retrievedBenchmark2);
    }

    [Fact]
    public async Task GetBenchmarkMappingsAsync_ReturnsAllBenchmarks()
    {
        _context.Portfolios.AddRange(CreatePortfolios());

        var benchmarks = CreateBenchmarks();

        _context.Benchmarks.AddRange(benchmarks);
        await _context.SaveChangesAsync();

        var retrievedBenchmarks = await _repository.GetBenchmarkMappingsAsync();

        Assert.Equal(benchmarks.Count, retrievedBenchmarks.Count());
    }


}