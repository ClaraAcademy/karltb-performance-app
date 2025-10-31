using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class InstrumentDayPerformance
{
    public int InstrumentId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal? DayPerformance { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo BankdayNavigation { get; set; } = null!;

    public virtual Instrument Instrument { get; set; } = null!;
}
