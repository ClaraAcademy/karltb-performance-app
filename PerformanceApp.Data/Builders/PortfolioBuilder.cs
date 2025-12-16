using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class PortfolioBuilder : IBuilder<Portfolio>
{
    private int _id = PortfolioBuilderDefaults.PortfolioId;
    private string _name = PortfolioBuilderDefaults.PortfolioName;
    private ApplicationUser _user = new ApplicationUser();
    private List<Portfolio> _benchmarks = new List<Portfolio>();
    private List<KeyFigureValue> _keyFigureValues = new List<KeyFigureValue>();
    private List<PortfolioPerformance> _performances = new List<PortfolioPerformance>();

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

    public PortfolioBuilder WithPerformances(List<PortfolioPerformance> performances)
    {
        _performances = performances;
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
            .WithPerformances(new List<PortfolioPerformance>(_performances))
            .Build();
    }

    public IEnumerable<Portfolio> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new PortfolioBuilder()
                .WithId(i + 1)
                .WithName($"Portfolio {i + 1}")
                .WithUser(_user)
                .WithBenchmarks(_benchmarks)
                .WithKeyFigureValues(_keyFigureValues)
                .WithPerformances(_performances)
                .Build();
        }
    }

    public Portfolio Build()
    {
        return new Portfolio
        {
            Id = _id,
            Name = _name,
            User = _user,
            BenchmarksNavigation = _benchmarks,
            KeyFigureValuesNavigation = _keyFigureValues,
            PortfolioPerformancesNavigation = _performances
        };

    }

}