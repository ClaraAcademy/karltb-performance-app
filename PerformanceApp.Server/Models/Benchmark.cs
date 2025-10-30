using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Benchmark
{
    public int PortfolioID { get; set; }
    public int BenchmarkID { get; set; }

    public Portfolio Portfolio { get; set; } = null!;

    public Portfolio BenchmarkPortfolio { get; set; } = null!;
}
