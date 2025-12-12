using PerformanceApp.Data.Models;

namespace PerformanceApp.Server.Test.Builders;

public class PortfolioBuilder
{
    private int _id = 1;
    private string _name = "Default Portfolio";
    private ApplicationUser _user = new ApplicationUser { Id = "userId1", UserName = "userName1" };
    private List<Portfolio> _benchmarks = new List<Portfolio>();

    public PortfolioBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PortfolioBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PortfolioBuilder WithUser(ApplicationUser user)
    {
        _user = user;
        return this;
    }

    public PortfolioBuilder WithBenchmarks(List<Portfolio> benchmarks)
    {
        _benchmarks = benchmarks;
        return this;
    }

    public PortfolioBuilder WithBenchmark(Portfolio benchmark)
    {
        _benchmarks.Add(benchmark);
        return this;
    }

    public Portfolio Build()
    {
        return new Portfolio
        {
            Id = _id,
            Name = _name,
            User = _user,
            PortfolioPortfolioBenchmarkEntityNavigation = new List<Benchmark>(
                _benchmarks.Select(b => new Benchmark { PortfolioId = _id, BenchmarkId = b.Id })
            )
        };

    }
}