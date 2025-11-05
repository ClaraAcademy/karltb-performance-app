using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class PortfolioBenchmarkDayPerformanceDTO
{
    public DateOnly Bankday { get; set; }

    public decimal PortfolioValue { get; set; }

    public decimal BenchmarkValue { get; set; }
}
