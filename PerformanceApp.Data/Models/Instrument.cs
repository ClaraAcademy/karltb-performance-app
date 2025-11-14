using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class Instrument
{
    public int InstrumentId { get; set; }

    public int? InstrumentTypeId { get; set; }

    public string? InstrumentName { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<InstrumentPerformance> InstrumentPerformancesNavigation { get; set; } = [];

    public virtual ICollection<InstrumentPrice> InstrumentPricesNavigation { get; set; } = [];

    public virtual InstrumentType? InstrumentTypeNavigation { get; set; }

    public virtual ICollection<Position> PositionsNavigation { get; set; } = [];

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
