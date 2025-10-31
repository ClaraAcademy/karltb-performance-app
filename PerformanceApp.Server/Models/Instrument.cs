namespace PerformanceApp.Server.Models;

public class Instrument
{
    public int InstrumentID { get; set; }
    public int InstrumentTypeID { get; set; }
    public string InstrumentName { get; set; } = null!;
    public InstrumentType InstrumentType { get; set; } = null!;
    public ICollection<InstrumentPrice> InstrumentPrices { get; set; } = null!;
}