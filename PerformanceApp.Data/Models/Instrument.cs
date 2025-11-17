using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class Instrument
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public string? Name { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<InstrumentPerformance> InstrumentPerformancesNavigation { get; set; } = [];

    public virtual ICollection<InstrumentPrice> InstrumentPricesNavigation { get; set; } = [];

    public virtual InstrumentType? InstrumentTypeNavigation { get; set; }

    public virtual ICollection<Position> PositionsNavigation { get; set; } = [];

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
