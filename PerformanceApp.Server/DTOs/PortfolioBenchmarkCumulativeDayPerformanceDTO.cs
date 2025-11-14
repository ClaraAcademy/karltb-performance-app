using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Dtos;

public partial class PortfolioBenchmarkCumulativeDayPerformanceDTO
{
    public DateOnly Bankday { get; set; }

    public decimal PortfolioValue { get; set; }

    public decimal BenchmarkValue { get; set; }
}
