namespace PerformanceApp.Data.Models;

public class PerformanceTypeInfo
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime Created { get; set; }
    public virtual ICollection<InstrumentPerformance> InstrumentPerformancesNavigation { get; set; } = [];
    public virtual ICollection<PortfolioPerformance> PortfolioPerformancesNavigation { get; set; } = [];
}