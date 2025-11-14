using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class DateInfo
{
    public DateOnly Bankday { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<InstrumentPerformance> InstrumentPerformancesPeriodStartNavigation { get; set; } = [];

    public virtual ICollection<InstrumentPerformance> InstrumentPerformancesPeriodEndNavigation { get; set; } = [];

    public virtual ICollection<InstrumentPrice> InstrumentPricesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioPerformance> PortfolioPerformancesPeriodStartNavigation { get; set; } = [];

    public virtual ICollection<PortfolioPerformance> PortfolioPerformancesPeriodEndNavigation { get; set; } = [];

    public virtual ICollection<PortfolioValue> PortfolioValuesNavigation { get; set; } = [];

    public virtual ICollection<PositionValue> PositionValuesNavigation { get; set; } = [];

    public virtual ICollection<Position> PositionsNavigation { get; set; } = [];

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
