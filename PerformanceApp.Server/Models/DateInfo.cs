namespace PerformanceApp.Server.Models;

public class DateInfo
{
    public DateTime Bankday { get; set; }

    public ICollection<InstrumentPrice> InstrumentPrices { get; set; } = null!;
}