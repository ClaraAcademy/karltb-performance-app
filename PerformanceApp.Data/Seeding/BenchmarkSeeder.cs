using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

public class BenchmarkSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly PortfolioRepository _portfolioRepository = new(context);
    private readonly BenchmarkRepository _benchmarkRepository = new(context);

    private Benchmark MapToBenchmark((string, string) pair)
    {
        var portfolio = _portfolioRepository.GetPortfolio(pair.Item1)!;
        var benchmark = _portfolioRepository.GetPortfolio(pair.Item2)!;

        return new Benchmark { PortfolioId = portfolio.PortfolioId, BenchmarkId = benchmark.PortfolioId };
    }

    public void Seed()
    {
        List<string> portfolios = ["Portfolio A", "Portfolio B"];
        List<string> benchmarks = ["Benchmark A", "Benchmark B"];

        var benchmarkMappings = portfolios.Zip(benchmarks)
            .Select(MapToBenchmark)
            .ToList();

        _benchmarkRepository.AddBenchmarkMappings(benchmarkMappings);

        _context.SaveChanges();
    }

}