using PerformanceApp.Data.Constants;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public class IndexPositionBuilderDefaults : PositionBuilderDefaults
{
    public static readonly decimal Proportion = 0.25m;
    public new static readonly Instrument InstrumentNavigation = new InstrumentBuilder()
        .WithId(10000)
        .WithInstrumentTypeNavigation(
            new InstrumentTypeBuilder()
                .WithId(10000)
                .WithName(InstrumentTypeConstants.Index)
                .Build()
        )
        .Build();
}