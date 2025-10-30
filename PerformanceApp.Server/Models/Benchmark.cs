using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Benchmark
{
    public int PortfolioID { get; set; }
    public int BenchmarkID { get; set; }

    public DateTime Created { get; set; }
}
