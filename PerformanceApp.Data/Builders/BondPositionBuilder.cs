using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class BondPositionBuilder : PositionBuilder
{
    private new decimal _nominal = Defaults.BondPositionBuilderDefaults.Nominal;

    public BondPositionBuilder WithNominal(decimal nominal)
    {
        _nominal = nominal;
        return this;
    }

    public override Position Build()
    {
        base._nominal = _nominal;
        // _instrument = Defaults.BondPositionBuilderDefaults.InstrumentNavigation;
        return base.Build();
    }
}