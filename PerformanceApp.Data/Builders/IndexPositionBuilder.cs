using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class IndexPositionBuilder : PositionBuilder
{
    private new decimal _proportion = Defaults.IndexPositionBuilderDefaults.Proportion;

    public IndexPositionBuilder WithProportion(decimal proportion)
    {
        _proportion = proportion;
        return this;
    }

    public override Position Build()
    {
        base._proportion = _proportion;
        // _instrument = Defaults.IndexPositionBuilderDefaults.InstrumentNavigation;
        return base.Build();
    }
}