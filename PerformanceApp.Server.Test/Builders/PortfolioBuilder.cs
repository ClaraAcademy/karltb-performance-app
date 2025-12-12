using PerformanceApp.Data.Models;
using PerformanceApp.Server.Test.Builders.Interface;

namespace PerformanceApp.Server.Test.Builders;

public class PortfolioBuilder : IBuilder<Portfolio>
{
    private int _id = 1;
    private string _name = "Default Portfolio";
    private ApplicationUser _user = new ApplicationUser { Id = "userId1", UserName = "userName1" };
    private List<Portfolio> _benchmarks = new List<Portfolio>();
    private List<KeyFigureValue> _keyFigureValues = new List<KeyFigureValue>();

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

    public PortfolioBuilder WithKeyFigureValues(List<KeyFigureValue> keyFigureValues)
    {
        _keyFigureValues = keyFigureValues;
        return this;
    }

    public Portfolio Clone()
    {
        return new PortfolioBuilder()
            .WithId(_id)
            .WithName(_name)
            .WithUser(_user)
            .WithBenchmarks(new List<Portfolio>(_benchmarks))
            .WithKeyFigureValues(new List<KeyFigureValue>(_keyFigureValues))
            .Build();
    }

    public IEnumerable<Portfolio> Many(int count)
    {
        return Enumerable.Range(1, count).Select(i => new PortfolioBuilder()
            .WithId(_id + i - 1)
            .WithName($"{_name} {i}")
            .WithUser(_user)
            .WithBenchmarks(new List<Portfolio>(_benchmarks))
            .WithKeyFigureValues(new List<KeyFigureValue>(_keyFigureValues))
            .Build());
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
            ),
            KeyFigureValuesNavigation = _keyFigureValues

        };

    }

}