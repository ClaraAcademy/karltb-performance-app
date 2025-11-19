namespace PerformanceApp.Data.Models;

public partial class InstrumentPrice
{
    public int InstrumentId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal Price { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo BankdayNavigation { get; set; } = null!;

    public virtual Instrument InstrumentNavigation { get; set; } = null!;
}
