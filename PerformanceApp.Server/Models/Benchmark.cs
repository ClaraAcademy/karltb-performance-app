using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Benchmark
{
    public int PortfolioId { get; set; }

    public int BenchmarkId { get; set; }

    public DateTime Created { get; set; }

    public virtual Portfolio BenchmarkNavigation { get; set; } = null!;

    public virtual Portfolio Portfolio { get; set; } = null!;
}
