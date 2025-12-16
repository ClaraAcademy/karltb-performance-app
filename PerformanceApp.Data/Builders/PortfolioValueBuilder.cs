using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class PortfolioValueBuilder : IBuilder<PortfolioValue>
{
    private int _portfolioId = PortfolioValueBuilderDefaults.PortfolioId;
    private decimal? _value = PortfolioValueBuilderDefaults.Value;
    private DateOnly _bankday = PortfolioValueBuilderDefaults.Bankday;

    public PortfolioValueBuilder WithPortfolioId(int portfolioId)
    {
        _portfolioId = portfolioId;
        return this;
    }

    public PortfolioValueBuilder WithValue(decimal? value)
    {
        _value = value;
        return this;
    }

    public PortfolioValueBuilder WithBankday(DateOnly bankday)
    {
        _bankday = bankday;
        return this;
    }

    public PortfolioValue Build()
    {
        return new PortfolioValue
        {
            PortfolioId = _portfolioId,
            Value = _value,
            Bankday = _bankday
        };
    }

    public PortfolioValue Clone()
    {
        return new PortfolioValueBuilder()
            .WithPortfolioId(_portfolioId)
            .WithValue(_value)
            .WithBankday(_bankday)
            .Build();
    }

    public IEnumerable<PortfolioValue> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new PortfolioValueBuilder()
                .WithPortfolioId(_portfolioId + i)
                .WithValue(_value + i * 10m)
                .WithBankday(_bankday.AddDays(i))
                .Build();
        }
    }
}