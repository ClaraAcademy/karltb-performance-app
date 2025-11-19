namespace PerformanceApp.Data.Models;

public partial class Portfolio
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public string? UserID { get; set; }

    public virtual ApplicationUser? User { get; set; }

    public virtual ICollection<Benchmark> BenchmarkBenchmarksNavigation { get; set; } = [];

    public virtual ICollection<Benchmark> BenchmarkPortfoliosNavigation { get; set; } = [];

    public virtual ICollection<KeyFigureValue> KeyFigureValuesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioPerformance> PortfolioPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioValue> PortfolioValuesNavigation { get; set; } = [];

    public virtual ICollection<Position> PositionsNavigation { get; set; } = [];

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
