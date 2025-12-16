using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public class PositionBuilderDefaults
{
    public static readonly Portfolio PortfolioNavigation = new PortfolioBuilder().Build();
    public static readonly Instrument InstrumentNavigation = new InstrumentBuilder().Build();
    public static readonly DateInfo BankdayNavigation = new DateInfoBuilder().Build();
    public static readonly int Id = 1;
    public static readonly int PortfolioId = PortfolioNavigation.Id;
    public static readonly int InstrumentId = InstrumentNavigation.Id;
    public static readonly DateOnly Bankday = DateInfoBuilderDefaults.Bankday;
    public static readonly List<PositionValue> PositionValuesNavigation = new PositionValueBuilder().Many(1).ToList();
}