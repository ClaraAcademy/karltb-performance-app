namespace PerformanceApp.Server.Models;

public class Position
{
    public int PortfolioID { get; set; }
    public string PorttfolioName { get; set; } = null!;
    public int InstrumentID { get; set; }
    public string InstrumentName { get; set; } = null!;
    public DateTime Bankday { get; set; }

    public Portfolio Portfolio { get; set; } = null!;
    public Instrument Instrument { get; set; } = null!;
    public DateInfo DateInfo { get; set; } = null!;
}