using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Data.Mappers;

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
            .Select(BenchmarkMapper.Map)
            .ToList();

        await _benchmarkRepository.AddBenchmarkMappingsAsync(benchmarkMappings);
    }

}