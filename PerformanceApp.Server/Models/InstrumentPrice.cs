namespace PerformanceApp.Server.Models;

public class InstrumentPrice
{
    public int InstrumentID { get; set; }
    public DateTime Bankday { get; set; }

    public decimal Price { get; set; }

    public Instrument Instrument { get; set; } = null!;
    public DateInfo DateInfo { get; set; } = null!;
}