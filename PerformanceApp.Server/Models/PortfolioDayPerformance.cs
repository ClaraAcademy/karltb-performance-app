using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class PortfolioDayPerformance
{
    public int PortfolioId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal? DayPerformance { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo BankdayNavigation { get; set; } = null!;

    public virtual Portfolio PortfolioNavigation { get; set; } = null!;
}
