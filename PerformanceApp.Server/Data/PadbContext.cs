using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.Models;

namespace PerformanceApp.Server.Data;

public partial class PadbContext : DbContext
{
    public PadbContext()
    {
    }

    public PadbContext(DbContextOptions<PadbContext> options)
        : base(options)
    {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benchmark>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.BenchmarkId });

            entity.ToTable("Benchmark", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.BenchmarkId).HasColumnName("BenchmarkID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.BenchmarkNavigation).WithMany(p => p.BenchmarkBenchmarkNavigations)
                .HasForeignKey(d => d.BenchmarkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Benchmark_BenchmarkID");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.BenchmarkPortfolios)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_Benchmark_PortfolioID");
        });

        modelBuilder.Entity<DateInfo>(entity =>
        {
            entity.HasKey(e => e.Bankday);

            entity.ToTable("DateInfo", "padb");

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.ToTable("Instrument", "padb");

            entity.HasIndex(e => e.InstrumentName, "UQ_Instrument_InstrumentName").IsUnique();

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentName).HasMaxLength(100);
            entity.Property(e => e.InstrumentTypeId).HasColumnName("InstrumentTypeID");

            entity.HasOne(d => d.InstrumentType).WithMany(p => p.Instruments)
                .HasForeignKey(d => d.InstrumentTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Instrument_InstrumentTypeID");
        });

        modelBuilder.Entity<InstrumentDayPerformance>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.Bankday });

            entity.ToTable("InstrumentDayPerformance", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DayPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.InstrumentDayPerformances)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentDayPerformance_Bankday");

            entity.HasOne(d => d.Instrument).WithMany(p => p.InstrumentDayPerformances)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentDayPerformance_InstrumentID");
        });

        modelBuilder.Entity<InstrumentHalfYearPerformance>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("InstrumentHalfYearPerformance", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HalfYearPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.Instrument).WithMany(p => p.InstrumentHalfYearPerformances)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentHalfYearPerformance_InstrumentID");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.InstrumentHalfYearPerformancePeriodEndNavigations)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentHalfYearPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.InstrumentHalfYearPerformancePeriodStartNavigations)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentHalfYearPerformance_PeriodStart");
        });

        modelBuilder.Entity<InstrumentMonthPerformance>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("InstrumentMonthPerformance", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MonthPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.Instrument).WithMany(p => p.InstrumentMonthPerformances)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentMonthPerformance_InstrumentID");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.InstrumentMonthPerformancePeriodEndNavigations)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentMonthPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.InstrumentMonthPerformancePeriodStartNavigations)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentMonthPerformance_PeriodStart");
        });

        modelBuilder.Entity<InstrumentPrice>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.Bankday });

            entity.ToTable("InstrumentPrice", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.InstrumentPrices)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentPrice_Bankday");

            entity.HasOne(d => d.Instrument).WithMany(p => p.InstrumentPrices)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentPrice_InstrumentID");
        });

        modelBuilder.Entity<InstrumentType>(entity =>
        {
            entity.ToTable("InstrumentType", "padb");

            entity.Property(e => e.InstrumentTypeId).HasColumnName("InstrumentTypeID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentTypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<KeyFigureInfo>(entity =>
        {
            entity.HasKey(e => e.KeyFigureId);

            entity.ToTable("KeyFigureInfo", "padb");

            entity.HasIndex(e => e.KeyFigureName, "UQ_KeyFigureInfo_KeyFigureName").IsUnique();

            entity.Property(e => e.KeyFigureId).HasColumnName("KeyFigureID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.KeyFigureName).HasMaxLength(100);
        });

        modelBuilder.Entity<KeyFigureValue>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.KeyFigureId });

            entity.ToTable("KeyFigureValue", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.KeyFigureId).HasColumnName("KeyFigureID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.KeyFigureValue1)
                .HasColumnType("decimal(24, 16)")
                .HasColumnName("KeyFigureValue");

            entity.HasOne(d => d.KeyFigure).WithMany(p => p.KeyFigureValues)
                .HasForeignKey(d => d.KeyFigureId)
                .HasConstraintName("FK_KeyFigureValue_KeyFigureID");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.KeyFigureValues)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_KeyFigureValue_PortfolioID");
        });

        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.ToTable("Portfolio", "padb");

            entity.HasIndex(e => e.PortfolioName, "UQ_Portfolio_PortfolioName").IsUnique();

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PortfolioName).HasMaxLength(100);
        });

        modelBuilder.Entity<PortfolioCumulativeDayPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.Bankday });

            entity.ToTable("PortfolioCumulativeDayPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CumulativeDayPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PortfolioCumulativeDayPerformances)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioCumulativeDayPerformance_Bankday");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.PortfolioCumulativeDayPerformances)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioCumulativeDayPerformance_PortfolioID");
        });

        modelBuilder.Entity<PortfolioDayPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.Bankday });

            entity.ToTable("PortfolioDayPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DayPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PortfolioDayPerformances)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioDayPerformance_Bankday");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.PortfolioDayPerformances)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioDayPerformance_PortfolioID");
        });

        modelBuilder.Entity<PortfolioHalfYearPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("PortfolioHalfYearPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HalfYearPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.PortfolioHalfYearPerformancePeriodEndNavigations)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioHalfYearPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.PortfolioHalfYearPerformancePeriodStartNavigations)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioHalfYearPerformance_PeriodStart");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.PortfolioHalfYearPerformances)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioHalfYearPerformance_PortfolioID");
        });

        modelBuilder.Entity<PortfolioMonthPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("PortfolioMonthPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MonthPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.PortfolioMonthPerformancePeriodEndNavigations)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioMonthPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.PortfolioMonthPerformancePeriodStartNavigations)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioMonthPerformance_PeriodStart");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.PortfolioMonthPerformances)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioMonthPerformance_PortfolioID");
        });

        modelBuilder.Entity<PortfolioValue>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.Bankday });

            entity.ToTable("PortfolioValue", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PortfolioValue1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PortfolioValue");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PortfolioValues)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioValue_Bankday");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.PortfolioValues)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioValue_PortfolioID");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position", "padb");

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Nominal).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Proportion).HasColumnType("decimal(5, 4)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.Positions)
                .HasForeignKey(d => d.Bankday)
                .HasConstraintName("FK_Position_Bankday");

            entity.HasOne(d => d.Instrument).WithMany(p => p.Positions)
                .HasForeignKey(d => d.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Position_InstrumentID");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.Positions)
                .HasForeignKey(d => d.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Position_PortfolioID");
        });

        modelBuilder.Entity<PositionValue>(entity =>
        {
            entity.HasKey(e => new { e.PositionId, e.Bankday });

            entity.ToTable("PositionValue", "padb");

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PositionValue1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PositionValue");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PositionValues)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PositionValue_Bankday");

            entity.HasOne(d => d.Position).WithMany(p => p.PositionValues)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_PositionValue_PositionID");
        });

        modelBuilder.Entity<Staging>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Staging", "padb");

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentName).HasMaxLength(100);
            entity.Property(e => e.InstrumentType).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction", "padb");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Nominal).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Proportion).HasColumnType("decimal(5, 4)");
            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.Bankday)
                .HasConstraintName("FK_Transaction_Bankday");

            entity.HasOne(d => d.Instrument).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Transaction_InstrumentID");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Transaction_PortfolioID");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Transaction_TransactionTypeID");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.ToTable("TransactionType", "padb");

            entity.HasIndex(e => e.TransactionTypeName, "UQ_TransactionType_TransactionTypeName").IsUnique();

            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");
            entity.Property(e => e.TransactionTypeName).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
