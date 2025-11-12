using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class PortfolioCumulativeDayPerformance
{
    public int PortfolioId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal? CumulativeDayPerformance { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo BankdayNavigation { get; set; } = null!;

    public virtual Portfolio PortfolioNavigation { get; set; } = null!;
}
