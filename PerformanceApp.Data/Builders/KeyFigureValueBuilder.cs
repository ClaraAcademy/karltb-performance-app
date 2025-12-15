using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class KeyFigureValueBuilder : IBuilder<KeyFigureValue>
{
    private KeyFigureInfo _keyFigureInfo = new KeyFigureInfoBuilder().Build();
    private decimal _value = KeyFigureValueBuilderDefaults.Value;
    private Portfolio _portfolio = new PortfolioBuilder().Build();

    public KeyFigureValueBuilder WithKeyFigureInfo(KeyFigureInfo keyFigureInfo)
    {
        _keyFigureInfo = keyFigureInfo;
        return this;
    }

    public KeyFigureValueBuilder WithValue(decimal value)
    {
        _value = value;
        return this;
    }

    public KeyFigureValue Build()
    {
        return new KeyFigureValue
        {
            KeyFigureId = _keyFigureInfo.Id,
            KeyFigureInfoNavigation = _keyFigureInfo,
            Value = _value,
            PortfolioId = _portfolio.Id,
            PortfolioNavigation = _portfolio
        };
    }

    public KeyFigureValue Clone()
    {
        return new KeyFigureValueBuilder()
            .WithKeyFigureInfo(_keyFigureInfo)
            .WithValue(_value)
            .Build();
    }

    public IEnumerable<KeyFigureValue> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new KeyFigureValueBuilder()
                .WithKeyFigureInfo(_keyFigureInfo)
                .WithValue(_value + i)
                .Build();
        }
    }
}