﻿using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class PortfolioHalfYearPerformance
{
    public int PortfolioId { get; set; }

    public DateOnly PeriodStart { get; set; }

    public DateOnly PeriodEnd { get; set; }

    public decimal? HalfYearPerformance { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo PeriodEndNavigation { get; set; } = null!;

    public virtual DateInfo PeriodStartNavigation { get; set; } = null!;

    public virtual Portfolio Portfolio { get; set; } = null!;
}
