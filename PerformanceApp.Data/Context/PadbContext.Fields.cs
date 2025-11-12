using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context;

public partial class PadbContext
{
    public virtual DbSet<Benchmark> Benchmarks { get; set; }
    public virtual DbSet<DateInfo> DateInfos { get; set; }
    public virtual DbSet<Instrument> Instruments { get; set; }
    public virtual DbSet<InstrumentDayPerformance> InstrumentDayPerformances { get; set; }
    public virtual DbSet<InstrumentHalfYearPerformance> InstrumentHalfYearPerformances { get; set; }
    public virtual DbSet<InstrumentMonthPerformance> InstrumentMonthPerformances { get; set; }
    public virtual DbSet<InstrumentPrice> InstrumentPrices { get; set; }
    public virtual DbSet<InstrumentType> InstrumentTypes { get; set; }
    public virtual DbSet<KeyFigureInfo> KeyFigureInfos { get; set; }
    public virtual DbSet<KeyFigureValue> KeyFigureValues { get; set; }
    public virtual DbSet<Portfolio> Portfolios { get; set; }
    public virtual DbSet<PortfolioCumulativeDayPerformance> PortfolioCumulativeDayPerformances { get; set; }
    public virtual DbSet<PortfolioDayPerformance> PortfolioDayPerformances { get; set; }
    public virtual DbSet<PortfolioHalfYearPerformance> PortfolioHalfYearPerformances { get; set; }
    public virtual DbSet<PortfolioMonthPerformance> PortfolioMonthPerformances { get; set; }
    public virtual DbSet<PortfolioValue> PortfolioValues { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<PositionValue> PositionValues { get; set; }
    public virtual DbSet<Staging> Stagings { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<TransactionType> TransactionTypes { get; set; }
}