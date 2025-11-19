namespace PerformanceApp.Data.Models;

public partial class InstrumentType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<Instrument> InstrumentsNavigation { get; set; } = [];
}
