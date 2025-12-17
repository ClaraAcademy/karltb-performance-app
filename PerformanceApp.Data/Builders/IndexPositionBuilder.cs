using PerformanceApp.Data.Constants;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class IndexPositionBuilder : PositionBuilder
{
    private new decimal _proportion = 0.25m;
    private new Instrument _instrument = new InstrumentBuilder()
        .WithInstrumentTypeNavigation(
            new InstrumentTypeBuilder()
                .WithName(InstrumentTypeConstants.Index)
                .Build()
        )
        .Build();

    public IndexPositionBuilder WithProportion(decimal proportion)
    {
        _proportion = proportion;
        return this;
    }

    public override Position Build()
    {
        base._proportion = _proportion;
        base._instrument = _instrument;
        return base.Build();
    }
}