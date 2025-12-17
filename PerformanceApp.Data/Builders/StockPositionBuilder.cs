using PerformanceApp.Data.Constants;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class StockPositionBuilder : PositionBuilder
{
    private new int _count = 10;
    private new Instrument _instrument = new InstrumentBuilder()
        .WithInstrumentTypeNavigation(
            new InstrumentTypeBuilder()
                .WithName(InstrumentTypeConstants.Stock)
                .Build()
        )
        .Build();

    public StockPositionBuilder WithCount(int count)
    {
        _count = count;
        return this;
    }

    public override Position Build()
    {
        base._count = _count;
        base._instrument = _instrument;
        return base.Build();
    }
}