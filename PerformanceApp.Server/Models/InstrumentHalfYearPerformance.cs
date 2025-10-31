using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class InstrumentHalfYearPerformance
{
    public int InstrumentId { get; set; }

    public DateOnly PeriodStart { get; set; }

    public DateOnly PeriodEnd { get; set; }

    public decimal? HalfYearPerformance { get; set; }

    public DateTime Created { get; set; }

    public virtual Instrument Instrument { get; set; } = null!;

    public virtual DateInfo PeriodEndNavigation { get; set; } = null!;

    public virtual DateInfo PeriodStartNavigation { get; set; } = null!;
}
