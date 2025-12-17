using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public class PositionBuilderDefaults
{
    public static readonly Portfolio PortfolioNavigation = new PortfolioBuilder().Build();
    public static readonly Instrument InstrumentNavigation = new InstrumentBuilder().Build();
    public static readonly int Id = 0;
    public static readonly DateOnly Bankday = DateInfoBuilderDefaults.Bankday;
}