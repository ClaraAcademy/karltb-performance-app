using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

public class BenchmarkSeeder(PadbContext context)
{
    private readonly PortfolioRepository _portfolioRepository = new(context);
    private readonly BenchmarkRepository _benchmarkRepository = new(context);

    private async Task<Benchmark> MapToBenchmark((string portfolioName, string benchmarkName) pair)
    {
        var portfolio = await _portfolioRepository.GetPortfolioAsync(pair.portfolioName);
        var benchmark = await _portfolioRepository.GetPortfolioAsync(pair.benchmarkName);

        return new Benchmark { PortfolioId = portfolio!.PortfolioId, BenchmarkId = benchmark!.PortfolioId };
    }

    public async Task Seed()
    {
        var portfolios = new List<string> { "Portfolio A", "Portfolio B" };
        var benchmarks = new List<string> { "Benchmark A", "Benchmark B" };

        var tasks = portfolios.Zip(benchmarks).Select(MapToBenchmark);
        var benchmarkMappings = await Task.WhenAll(tasks);

        await _benchmarkRepository.AddBenchmarkMappingsAsync(benchmarkMappings.ToList());
    }

}