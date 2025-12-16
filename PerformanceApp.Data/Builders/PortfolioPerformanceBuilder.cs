using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class PortfolioPerformanceBuilder : IBuilder<PortfolioPerformance>
{
    private int _id = PortfolioPerformanceBuilderDefaults.Id;
    private DateOnly _periodStart = PortfolioPerformanceBuilderDefaults.PeriodStart;
    private DateOnly _periodEnd = PortfolioPerformanceBuilderDefaults.PeriodEnd;
    private PerformanceType _performanceType = new PerformanceTypeBuilder().Build();
    private decimal _value = PortfolioPerformanceBuilderDefaults.Value;
    private int _portfolioId = new PortfolioBuilder().Build().Id;
    private Portfolio _portfolio = new PortfolioBuilder().Build();

    public PortfolioPerformanceBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PortfolioPerformanceBuilder WithPeriodStart(DateOnly periodStart)
    {
        _periodStart = periodStart;
        return this;
    }

    public PortfolioPerformanceBuilder WithPeriodEnd(DateOnly periodEnd)
    {
        _periodEnd = periodEnd;
        return this;
    }

    public PortfolioPerformanceBuilder WithPerformanceType(PerformanceType performanceType)
    {
        _performanceType = performanceType;
        return this;
    }

    public PortfolioPerformanceBuilder WithValue(decimal value)
    {
        _value = value;
        return this;
    }

    public PortfolioPerformanceBuilder WithPortfolioId(int portfolioId)
    {
        _portfolioId = portfolioId;
        return this;
    }

    public PortfolioPerformanceBuilder WithPortfolio(Portfolio portfolio)
    {
        _portfolio = portfolio;
        _portfolioId = portfolio.Id;
        return this;
    }

    public PortfolioPerformance Build()
    {
        return new PortfolioPerformance
        {
            TypeId = _id,
            PeriodStart = _periodStart,
            PeriodEnd = _periodEnd,
            PerformanceTypeNavigation = _performanceType,
            Value = _value,
            PortfolioId = _portfolioId,
            PortfolioNavigation = _portfolio
        };
    }

    public PortfolioPerformance Clone()
    {
        return new PortfolioPerformanceBuilder()
            .WithId(_id)
            .WithPeriodStart(_periodStart)
            .WithPeriodEnd(_periodEnd)
            .WithPerformanceType(_performanceType)
            .WithValue(_value)
            .WithPortfolio(_portfolio)
            .Build();
    }

    public IEnumerable<PortfolioPerformance> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new PortfolioPerformanceBuilder()
                .WithId(_id + i)
                .WithPeriodStart(_periodStart.AddDays(i))
                .WithPeriodEnd(_periodEnd.AddDays(i))
                .WithPerformanceType(_performanceType)
                .WithValue(_value + i)
                .WithPortfolio(_portfolio)
                .Build();
        }
    }
}