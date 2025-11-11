using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Portfolio
{
    public int PortfolioId { get; set; }

    public string PortfolioName { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<Benchmark> BenchmarkBenchmarkNavigations { get; set; } = [];

    public virtual ICollection<Benchmark> BenchmarkPortfolios { get; set; } = [];

    public virtual ICollection<KeyFigureValue> KeyFigureValues { get; set; } =[];

    public virtual ICollection<PortfolioCumulativeDayPerformance> PortfolioCumulativeDayPerformances { get; set; } = [];

    public virtual ICollection<PortfolioDayPerformance> PortfolioDayPerformances { get; set; } = [];

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformances { get; set; } = [];

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformances { get; set; } = [];

    public virtual ICollection<PortfolioValue> PortfolioValues { get; set; } = [];

    public virtual ICollection<Position> Positions { get; set; } = [];

    public virtual ICollection<Transaction> Transactions { get; set; } = [];
}
