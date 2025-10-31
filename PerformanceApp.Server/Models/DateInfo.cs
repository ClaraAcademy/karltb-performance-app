using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class DateInfo
{
    public DateOnly Bankday { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<InstrumentDayPerformance> InstrumentDayPerformances { get; set; } = new List<InstrumentDayPerformance>();

    public virtual ICollection<InstrumentHalfYearPerformance> InstrumentHalfYearPerformancePeriodEndNavigations { get; set; } = new List<InstrumentHalfYearPerformance>();

    public virtual ICollection<InstrumentHalfYearPerformance> InstrumentHalfYearPerformancePeriodStartNavigations { get; set; } = new List<InstrumentHalfYearPerformance>();

    public virtual ICollection<InstrumentMonthPerformance> InstrumentMonthPerformancePeriodEndNavigations { get; set; } = new List<InstrumentMonthPerformance>();

    public virtual ICollection<InstrumentMonthPerformance> InstrumentMonthPerformancePeriodStartNavigations { get; set; } = new List<InstrumentMonthPerformance>();

    public virtual ICollection<InstrumentPrice> InstrumentPrices { get; set; } = new List<InstrumentPrice>();

    public virtual ICollection<PortfolioCumulativeDayPerformance> PortfolioCumulativeDayPerformances { get; set; } = new List<PortfolioCumulativeDayPerformance>();

    public virtual ICollection<PortfolioDayPerformance> PortfolioDayPerformances { get; set; } = new List<PortfolioDayPerformance>();

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformancePeriodEndNavigations { get; set; } = new List<PortfolioHalfYearPerformance>();

    public virtual ICollection<PortfolioHalfYearPerformance> PortfolioHalfYearPerformancePeriodStartNavigations { get; set; } = new List<PortfolioHalfYearPerformance>();

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformancePeriodEndNavigations { get; set; } = new List<PortfolioMonthPerformance>();

    public virtual ICollection<PortfolioMonthPerformance> PortfolioMonthPerformancePeriodStartNavigations { get; set; } = new List<PortfolioMonthPerformance>();

    public virtual ICollection<PortfolioValue> PortfolioValues { get; set; } = new List<PortfolioValue>();

    public virtual ICollection<PositionValue> PositionValues { get; set; } = new List<PositionValue>();

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
