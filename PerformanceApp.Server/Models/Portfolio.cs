using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Portfolio
{
    public int PortfolioId { get; set; }

    public string PortfolioName { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<Benchmark> BenchmarkBenchmarkNavigations { get; set; } = new List<Benchmark>();

    public virtual ICollection<Benchmark> BenchmarkPortfolios { get; set; } = new List<Benchmark>();

    public virtual ICollection<KeyFigureValue> KeyFigureValues { get; set; } = new List<KeyFigureValue>();

    public virtual ICollection<PortfolioCumulativeDayPerformance> PortfolioCumulativeDayPerformances { get; set; } = new List<PortfolioCumulativeDayPerformance>();

    public virtual ICollection<PortfolioDayPerformance> PortfolioDayPerformances { get; set; } = new List<PortfolioDayPerformance>();

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformances { get; set; } = new List<PortfolioHalfYearPerformance>();

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformances { get; set; } = new List<PortfolioMonthPerformance>();

    public virtual ICollection<PortfolioValue> PortfolioValues { get; set; } = new List<PortfolioValue>();

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
