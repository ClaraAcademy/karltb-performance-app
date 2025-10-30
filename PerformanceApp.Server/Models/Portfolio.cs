using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Portfolio
{
    public int PortfolioId { get; set; }

    public string PortfolioName { get; set; } = null!;

    public DateTime Created { get; set; }
}
