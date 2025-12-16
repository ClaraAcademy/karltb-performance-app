using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class InstrumentPerformanceBuilderDefaults
{
    public static readonly Instrument InstrumentNavigation = new InstrumentBuilder().Build();
    public static readonly int InstrumentId = InstrumentNavigation.Id;
    public static readonly PerformanceType PerformanceTypeNavigation = new PerformanceTypeBuilder().Build();
    public static readonly int TypeId = PerformanceTypeNavigation.Id;
    public static readonly DateOnly PeriodStart = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1));
    public static readonly DateOnly PeriodEnd = DateOnly.FromDateTime(DateTime.UtcNow);
    public static readonly decimal Value = 500.00m;
}