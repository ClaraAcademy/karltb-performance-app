using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Columns;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Context;
using PerformanceApp.Infrastructure.Context.Configuration.Entities;

namespace PerformanceApp.Infrastructure.Context;

public class PadbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public virtual DbSet<Benchmark> Benchmarks { get; set; }
    public virtual DbSet<DateInfo> DateInfos { get; set; }
    public virtual DbSet<Instrument> Instruments { get; set; }
    public virtual DbSet<InstrumentPerformance> InstrumentPerformances { get; set; }
    public virtual DbSet<InstrumentPrice> InstrumentPrices { get; set; }
    public virtual DbSet<InstrumentType> InstrumentTypes { get; set; }
    public virtual DbSet<KeyFigureInfo> KeyFigureInfos { get; set; }
    public virtual DbSet<KeyFigureValue> KeyFigureValues { get; set; }
    public virtual DbSet<Portfolio> Portfolios { get; set; }
    public virtual DbSet<PortfolioPerformance> PortfolioPerformances { get; set; }
    public virtual DbSet<PortfolioValue> PortfolioValues { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<PositionValue> PositionValues { get; set; }
    public virtual DbSet<Staging> Stagings { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<TransactionType> TransactionTypes { get; set; }
    public virtual DbSet<PerformanceType> PerformanceTypeInfos { get; set; }

    public PadbContext() { }
    public PadbContext(DbContextOptions<PadbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Initialize Identity
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(ContextConstants.Schema);

        // Configure columns
        modelBuilder.ConfigureCreatedColumns();
        modelBuilder.ConfigureIdColumns();

        // Initialize models
        modelBuilder.ConfigureBenchmark();
        modelBuilder.ConfigurePerformanceType();
        modelBuilder.ConfigureDateInfo();
        modelBuilder.ConfigureInstrument();
        modelBuilder.ConfigureInstrumentPerformance();
        modelBuilder.ConfigureInstrumentPrice();
        modelBuilder.ConfigureInstrumentType();
        modelBuilder.ConfigureKeyFigureInfo();
        modelBuilder.ConfigureKeyFigureValue();
        modelBuilder.ConfigurePortfolio();
        modelBuilder.ConfigurePortfolioPerformance();
        modelBuilder.ConfigurePortfolioValue();
        modelBuilder.ConfigurePosition();
        modelBuilder.ConfigurePositionValue();
        modelBuilder.ConfigureStaging();
        modelBuilder.ConfigureTransaction();
        modelBuilder.ConfigureTransactionType();
    }
}