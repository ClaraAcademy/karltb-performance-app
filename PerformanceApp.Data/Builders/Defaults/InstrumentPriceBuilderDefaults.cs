using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class InstrumentPriceBuilderDefaults
{
    public static readonly int InstrumentId = InstrumentBuilderDefaults.Id;
    public static readonly DateOnly Bankday = DateOnly.FromDateTime(DateTime.UtcNow);
    public static readonly decimal Price = 100.0m;
}