using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class KeyFigureValueBuilder
{
    private KeyFigureInfo _keyFigureInfo = new KeyFigureInfoBuilder().Build();
    private decimal _value = 100.0m;
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
}