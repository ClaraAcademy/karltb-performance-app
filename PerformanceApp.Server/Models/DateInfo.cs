using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class DateInfo
{
    public DateOnly Bankday { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<InstrumentDayPerformance> InstrumentDayPerformancesNavigation { get; set; } = [];

    public virtual ICollection<InstrumentHalfYearPerformance> InstrumentHalfYearPerformancePeriodEndsNavigation { get; set; } = [];

    public virtual ICollection<InstrumentHalfYearPerformance> InstrumentHalfYearPerformancePeriodStartsNavigation { get; set; } = [];

    public virtual ICollection<InstrumentMonthPerformance> InstrumentMonthPerformancePeriodEndsNavigation { get; set; } = [];

    public virtual ICollection<InstrumentMonthPerformance> InstrumentMonthPerformancePeriodStartsNavigation { get; set; } = [];

    public virtual ICollection<InstrumentPrice> InstrumentPricesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioCumulativeDayPerformance> PortfolioCumulativeDayPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioDayPerformance> PortfolioDayPerformancesNavigation { get; set; } = [];

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformancePeriodEndsNavigation { get; set; } = [];

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformancePeriodStartsNavigation { get; set; } = [];

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformancePeriodEndsNavigation { get; set; } = [];

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformancePeriodStartsNavigation { get; set; } = [];

    public virtual ICollection<PortfolioValue> PortfolioValuesNavigation { get; set; } = [];

    public virtual ICollection<PositionValue> PositionValuesNavigation { get; set; } = [];

    public virtual ICollection<Position> PositionsNavigation { get; set; } = [];

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
