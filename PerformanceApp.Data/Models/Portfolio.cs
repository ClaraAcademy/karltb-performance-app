using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class Portfolio
{
    public int PortfolioId { get; set; }

    public string PortfolioName { get; set; } = null!;

    public DateTime Created { get; set; }

    public string? UserID { get; set; }

    public virtual ApplicationUser? User { get; set; }

    public virtual ICollection<Benchmark> BenchmarkBenchmarksNavigation { get; set; } = [];

    public virtual ICollection<Benchmark> BenchmarkPortfoliosNavigation { get; set; } = [];

    public virtual ICollection<KeyFigureValue> KeyFigureValuesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioCumulativeDayPerformance> PortfolioCumulativeDayPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioDayPerformance> PortfolioDayPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioValue> PortfolioValuesNavigation { get; set; } = [];

    public virtual ICollection<Position> PositionsNavigation { get; set; } = [];

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
