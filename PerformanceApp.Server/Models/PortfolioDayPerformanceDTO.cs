using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class PortfolioDayPerformanceDTO
{
    public int PortfolioId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal Value { get; set; }
}
