using PerformanceApp.Data.Constants;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class BondPositionBuilder : PositionBuilder
{
    private new decimal _nominal = 1000m;
    private new Instrument _instrument = new InstrumentBuilder()
        .WithInstrumentTypeNavigation(
            new InstrumentTypeBuilder()
                .WithName(InstrumentTypeConstants.Bond)
                .Build()
        )
        .Build();

    public BondPositionBuilder WithNominal(decimal nominal)
    {
        _nominal = nominal;
        return this;
    }

    public override Position Build()
    {
        base._nominal = _nominal;
        base._instrument = _instrument;
        return base.Build();
    }
}