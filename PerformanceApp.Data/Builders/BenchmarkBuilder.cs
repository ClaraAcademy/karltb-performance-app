using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class BenchmarkBuilder : IBuilder<Benchmark>
{
    private int _portfolioId = BenchmarkBuilderDefaults.PortfolioId;
    private int _benchmarkId = BenchmarkBuilderDefaults.BenchmarkId;
    private Portfolio _portfolioPortfolioNavigation = new PortfolioBuilder()
        .WithId(BenchmarkBuilderDefaults.PortfolioId)
        .WithName($"Portfolio {BenchmarkBuilderDefaults.PortfolioId}")
        .Build();

    private Portfolio _benchmarkPortfolioNavigation = new PortfolioBuilder()
        .WithId(BenchmarkBuilderDefaults.BenchmarkId)
        .WithName($"Portfolio {BenchmarkBuilderDefaults.BenchmarkId}")
        .Build();

    public BenchmarkBuilder WithPortfolioId(int portfolioId)
    {
        _portfolioId = portfolioId;
        return this;
    }

    public BenchmarkBuilder WithBenchmarkId(int benchmarkId)
    {
        _benchmarkId = benchmarkId;
        return this;
    }

    public BenchmarkBuilder WithPortfolioPortfolioNavigation(Portfolio portfolio)
    {
        _portfolioPortfolioNavigation = portfolio;
        return this;
    }

    public BenchmarkBuilder WithBenchmarkPortfolioNavigation(Portfolio portfolio)
    {
        _benchmarkPortfolioNavigation = portfolio;
        return this;
    }

    public Benchmark Build()
    {
        return new Benchmark
        {
            PortfolioId = _portfolioId,
            BenchmarkId = _benchmarkId,
            PortfolioPortfolioNavigation = _portfolioPortfolioNavigation,
            BenchmarkPortfolioNavigation = _benchmarkPortfolioNavigation
        };
    }

    public Benchmark Clone()
    {
        return new BenchmarkBuilder()
            .WithPortfolioId(_portfolioId)
            .WithBenchmarkId(_benchmarkId)
            .WithPortfolioPortfolioNavigation(_portfolioPortfolioNavigation)
            .WithBenchmarkPortfolioNavigation(_benchmarkPortfolioNavigation)
            .Build();
    }

    public IEnumerable<Benchmark> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new BenchmarkBuilder()
                .WithPortfolioId(_portfolioId + i)
                .WithBenchmarkId(_benchmarkId + i)
                .WithPortfolioPortfolioNavigation(
                    new PortfolioBuilder()
                        .WithId(_portfolioId + i)
                        .WithName($"Portfolio {_portfolioId + i}")
                        .Build())
                .WithBenchmarkPortfolioNavigation(
                    new PortfolioBuilder()
                        .WithId(_benchmarkId + i)
                        .WithName($"Portfolio {_benchmarkId + i}")
                        .Build())
                .Build();
        }
    }
}