namespace PerformanceApp.Data.Models;

public class InstrumentPerformance
{
    public int InstrumentId { get; set; }
    public int TypeId { get; set; }
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public decimal Value { get; set; }
    public DateTime Created { get; set; }
    public virtual Instrument InstrumentNavigation { get; set; } = null!;
    public virtual PerformanceType PerformanceTypeNavigation { get; set; } = null!;
    public virtual DateInfo PeriodStartNavigation { get; set; } = null!;
    public virtual DateInfo PeriodEndNavigation { get; set; } = null!;
}