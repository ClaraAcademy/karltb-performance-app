using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Instrument
{
    public int InstrumentId { get; set; }

    public int? InstrumentTypeId { get; set; }

    public string? InstrumentName { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<InstrumentDayPerformance> InstrumentDayPerformances { get; set; } = new List<InstrumentDayPerformance>();

    public virtual ICollection<InstrumentHalfYearPerformance> InstrumentHalfYearPerformances { get; set; } = new List<InstrumentHalfYearPerformance>();

    public virtual ICollection<InstrumentMonthPerformance> InstrumentMonthPerformances { get; set; } = new List<InstrumentMonthPerformance>();

    public virtual ICollection<InstrumentPrice> InstrumentPrices { get; set; } = new List<InstrumentPrice>();

    public virtual InstrumentType? InstrumentType { get; set; }

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
