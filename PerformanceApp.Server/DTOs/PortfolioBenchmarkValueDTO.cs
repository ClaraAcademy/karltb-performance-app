using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Dtos;

public partial class PortfolioBenchmarkValueDTO
{
    public DateOnly Bankday { get; set; }

    public decimal PortfolioValue { get; set; }
    public decimal BenchmarkValue { get; set; }
}
