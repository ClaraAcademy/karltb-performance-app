using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class InstrumentPriceBuilderDefaults
{
    public static readonly Instrument InstrumentNavigation = new InstrumentBuilder().Build();
    public static readonly int InstrumentId = InstrumentNavigation.Id;
    public static readonly DateInfo BankdayNavigation = new DateInfoBuilder().Build();
    public static readonly DateOnly Bankday = BankdayNavigation.Bankday;
    public static readonly decimal Price = 100.0m;
}