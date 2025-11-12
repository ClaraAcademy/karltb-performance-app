using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class InstrumentMonthPerformance
{
    public int InstrumentId { get; set; }

    public DateOnly PeriodStart { get; set; }

    public DateOnly PeriodEnd { get; set; }

    public decimal? MonthPerformance { get; set; }

    public DateTime Created { get; set; }

    public virtual Instrument InstrumentNavigation { get; set; } = null!;

    public virtual DateInfo PeriodEndNavigation { get; set; } = null!;

    public virtual DateInfo PeriodStartNavigation { get; set; } = null!;
}
