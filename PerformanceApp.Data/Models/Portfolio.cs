using PerformanceApp.Data.Mappers;

namespace PerformanceApp.Data.Models;

public partial class Portfolio
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public string? UserID { get; set; }

    public IEnumerable<Portfolio> BenchmarksNavigation
    {
        get
        {
            return PortfolioPortfolioBenchmarkEntityNavigation
                .Select(b => b.BenchmarkPortfolioNavigation)
                .ToList();
        }
        set
        {
            PortfolioPortfolioBenchmarkEntityNavigation = value
                .Select(b => BenchmarkMapper.Map(this, b))
                .ToList();
        }
    }

    public IEnumerable<Portfolio> PortfoliosNavigation
    {
        get
        {
            return BenchmarkPortfolioBenchmarkEntityNavigation
                .Select(b => b.PortfolioPortfolioNavigation)
                .ToList();
        }
        set
        {
            BenchmarkPortfolioBenchmarkEntityNavigation = value
                .Select(p => BenchmarkMapper.Map(p, this))
                .ToList();
        }
    }

    public virtual ApplicationUser? User { get; set; }

    public virtual ICollection<Benchmark> BenchmarkPortfolioBenchmarkEntityNavigation { get; set; } = [];

    public virtual ICollection<Benchmark> PortfolioPortfolioBenchmarkEntityNavigation { get; set; } = [];

    public virtual ICollection<KeyFigureValue> KeyFigureValuesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioPerformance> PortfolioPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioValue> PortfolioValuesNavigation { get; set; } = [];

    public virtual ICollection<Position> PositionsNavigation { get; set; } = [];

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
