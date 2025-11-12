using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class Benchmark
{
    public int PortfolioId { get; set; }

    public int BenchmarkId { get; set; }

    public DateTime Created { get; set; }

    public virtual Portfolio BenchmarkPortfolioNavigation { get; set; } = null!;

    public virtual Portfolio PortfolioPortfolioNavigation { get; set; } = null!;
}
