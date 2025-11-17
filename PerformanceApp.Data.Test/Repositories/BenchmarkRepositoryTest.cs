using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class BenchmarkRepositoryTest : RepositoryTest
{
    [Fact]
    public async Task AddBenchmarkMappingAsync_AddsBenchmarkToDatabase()
    {
        var context = CreateContext();
        var repository = new BenchmarkRepository(context);

        var benchmark = new Benchmark { PortfolioId = 1, BenchmarkId = 3 };

        await repository.AddBenchmarkMappingAsync(benchmark);

        var retrievedBenchmark = await context.Benchmarks.FindAsync(1, 3);
        Assert.NotNull(retrievedBenchmark);
    }

    [Fact]
    public async Task AddBenchmarkMappingsAsync_AddsBenchmarksToDatabase()
    {
        var context = CreateContext();
        var repository = new BenchmarkRepository(context);

        var benchmarks = new List<Benchmark>
        {
            new Benchmark { PortfolioId = 1, BenchmarkId = 3 },
            new Benchmark { PortfolioId = 2, BenchmarkId = 4 }
        };

        await repository.AddBenchmarkMappingsAsync(benchmarks);

        var retrievedBenchmark1 = await context.Benchmarks.FindAsync(1, 3);
        var retrievedBenchmark2 = await context.Benchmarks.FindAsync(2, 4);
        Assert.NotNull(retrievedBenchmark1);
        Assert.NotNull(retrievedBenchmark2);
    }

    [Fact]
    public async Task GetBenchmarkMappingsAsync_ReturnsAllBenchmarks()
    {
        var context = CreateContext();
        var repository = new BenchmarkRepository(context);

        var portfolio1 = new Portfolio { Id = 1, Name = "Portfolio 1" };
        var portfolio2 = new Portfolio { Id = 2, Name = "Portfolio 2" };
        var portfolio3 = new Portfolio { Id = 3, Name = "Portfolio 3" };
        var portfolio4 = new Portfolio { Id = 4, Name = "Portfolio 4" };

        context.Portfolios.AddRange(portfolio1, portfolio2, portfolio3, portfolio4);

        var benchmark1 = new Benchmark { PortfolioId = 1, BenchmarkId = 3 };
        var benchmark2 = new Benchmark { PortfolioId = 2, BenchmarkId = 4 };

        context.Benchmarks.AddRange(benchmark1, benchmark2);
        await context.SaveChangesAsync();

        var benchmarks = await repository.GetBenchmarkMappingsAsync();

        Assert.Equal(2, benchmarks.Count());
    }


}