using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class InstrumentPerformanceBuilderDefaults
{
    public static readonly Instrument InstrumentNavigation = new InstrumentBuilder().Build();
    public static readonly PerformanceType PerformanceTypeNavigation = new PerformanceTypeBuilder().Build();
    public static readonly DateInfo PeriodStartNavigation = new DateInfoBuilder()
        .WithBankday(
           DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1))
        ).Build();
    public static readonly DateInfo PeriodEndNavigation = new DateInfoBuilder()
        .WithBankday(
            DateOnly.FromDateTime(DateTime.UtcNow)
        ).Build();
    public static readonly decimal Value = 500.00m;
}