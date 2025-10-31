namespace PerformanceApp.Server.Models;

public class InstrumentType
{
    public int InstrumentTypeID { get; set; }
    public string InstrumentTypeName { get; set; } = null!;

    public ICollection<Instrument> Instruments { get; set; } = null!;
}