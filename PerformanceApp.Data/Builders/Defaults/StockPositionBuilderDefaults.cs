using PerformanceApp.Data.Models;
using PerformanceApp.Data.Constants;

namespace PerformanceApp.Data.Builders.Defaults;

public class StockPositionBuilderDefaults : PositionBuilderDefaults
{
    public static readonly int Count = 100;
    public new static readonly Instrument InstrumentNavigation = new InstrumentBuilder()
        .WithInstrumentTypeNavigation(
            new InstrumentTypeBuilder()
                .WithName(InstrumentTypeConstants.Stock)
                .Build()
        )
        .Build();
}