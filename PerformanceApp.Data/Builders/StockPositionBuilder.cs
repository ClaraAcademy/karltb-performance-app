using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class StockPositionBuilder : PositionBuilder
{
    private new int _count = Defaults.StockPositionBuilderDefaults.Count;

    public StockPositionBuilder WithCount(int count)
    {
        _count = count;
        return this;
    }

    public override Position Build()
    {
        base._count = _count;
        _instrumentNavigation = Defaults.StockPositionBuilderDefaults.InstrumentNavigation;
        return base.Build();
    }
}