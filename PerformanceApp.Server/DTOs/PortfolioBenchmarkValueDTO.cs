using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.DTOs;

public partial class PortfolioBenchmarkValueDTO
{
    public DateOnly Bankday { get; set; }

    public decimal PortfolioValue { get; set; }
    public decimal BenchmarkValue { get; set; }
}
