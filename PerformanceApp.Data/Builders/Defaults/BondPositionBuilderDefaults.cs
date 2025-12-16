using PerformanceApp.Data.Models;
using PerformanceApp.Data.Constants;

namespace PerformanceApp.Data.Builders.Defaults;

public class BondPositionBuilderDefaults : PositionBuilderDefaults
{
    public static readonly decimal Nominal = 1000m;
    public new static readonly Instrument InstrumentNavigation = new InstrumentBuilder()
        .WithInstrumentTypeNavigation(
            new InstrumentTypeBuilder()
                .WithName(InstrumentTypeConstants.Bond)
                .Build()
        )
        .Build();
}