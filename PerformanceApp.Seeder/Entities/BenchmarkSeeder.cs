using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Entities;

public class BenchmarkSeeder(PadbContext context)
{
    private readonly PortfolioRepository _portfolioRepository = new(context);
    private readonly BenchmarkRepository _benchmarkRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var benchmarks = await _benchmarkRepository.GetBenchmarkMappingsAsync();

        return benchmarks.Any();
    }

    private Benchmark MapToBenchmark((Portfolio, Portfolio) pair)
    {
        var (portfolio, benchmark) = pair;
        return new Benchmark
        {
            PortfolioId = portfolio.Id,
            BenchmarkId = benchmark.Id
        };
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();
        if (exists)
        {
            return;
        }

        var portfolioNames = new List<string> { PortfolioData.PortfolioA, PortfolioData.PortfolioB };
        var benchmarkNames = new List<string> { PortfolioData.BenchmarkA, PortfolioData.BenchmarkB };

        var portfolios = await _portfolioRepository.GetPortfoliosAsync(portfolioNames);
        var benchmarks = await _portfolioRepository.GetPortfoliosAsync(benchmarkNames);

        var benchmarkMappings = portfolios.Zip(benchmarks)
            .Select(MapToBenchmark)
            .ToList();

        await _benchmarkRepository.AddBenchmarkMappingsAsync(benchmarkMappings);
    }

}