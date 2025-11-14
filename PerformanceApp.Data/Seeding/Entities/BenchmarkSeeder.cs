using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Entities;

public class BenchmarkSeeder(PadbContext context)
{
    private readonly PortfolioRepository _portfolioRepository = new(context);
    private readonly BenchmarkRepository _benchmarkRepository = new(context);

    private Benchmark MapToBenchmark((Portfolio, Portfolio) pair)
    {
        var (portfolio, benchmark) = pair;
        return new Benchmark
        {
            PortfolioId = portfolio.PortfolioId,
            BenchmarkId = benchmark.PortfolioId
        };
    }

    public async Task Seed()
    {
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