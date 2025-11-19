namespace PerformanceApp.Data.Models;

public partial class PortfolioValue
{
    public int PortfolioId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal? Value { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo BankdayNavigation { get; set; } = null!;

    public virtual Portfolio PortfolioNavigation { get; set; } = null!;
}
