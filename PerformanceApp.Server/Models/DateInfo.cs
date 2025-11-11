using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class DateInfo
{
    public DateOnly Bankday { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<InstrumentDayPerformance> InstrumentDayPerformances { get; set; } = [];

    public virtual ICollection<InstrumentHalfYearPerformance> InstrumentHalfYearPerformancePeriodEndNavigations { get; set; } = [];

    public virtual ICollection<InstrumentHalfYearPerformance> InstrumentHalfYearPerformancePeriodStartNavigations { get; set; } = [];

    public virtual ICollection<InstrumentMonthPerformance> InstrumentMonthPerformancePeriodEndNavigations { get; set; } = [];

    public virtual ICollection<InstrumentMonthPerformance> InstrumentMonthPerformancePeriodStartNavigations { get; set; } = [];

    public virtual ICollection<InstrumentPrice> InstrumentPrices { get; set; } = [];

    public virtual ICollection<PortfolioCumulativeDayPerformance> PortfolioCumulativeDayPerformances { get; set; } = [];

    public virtual ICollection<PortfolioDayPerformance> PortfolioDayPerformances { get; set; } = [];

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformancePeriodEndNavigations { get; set; } = [];

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformancePeriodStartNavigations { get; set; } = [];

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformancePeriodEndNavigations { get; set; } = [];

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformancePeriodStartNavigations { get; set; } = [];

    public virtual ICollection<PortfolioValue> PortfolioValues { get; set; } = [];

    public virtual ICollection<PositionValue> PositionValues { get; set; } = [];

    public virtual ICollection<Position> Positions { get; set; } = [];

    public virtual ICollection<Transaction> Transactions { get; set; } = [];
}
