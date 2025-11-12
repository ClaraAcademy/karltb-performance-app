using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Server.Models;

namespace PerformanceApp.Server.Data;

public partial class PadbContext
{
    private static void ConfigureBenchmark(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benchmark>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.BenchmarkId });

            entity.ToTable("Benchmark", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.BenchmarkId).HasColumnName("BenchmarkID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.BenchmarkPortfolioNavigation).WithMany(p => p.BenchmarkBenchmarksNavigation)
                .HasForeignKey(d => d.BenchmarkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Benchmark_BenchmarkID");

            entity.HasOne(d => d.PortfolioPortfolioNavigation).WithMany(p => p.BenchmarkPortfoliosNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_Benchmark_PortfolioID");
        });
    }

    private static void ConfigureDateInfo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DateInfo>(entity =>
        {
            entity.HasKey(e => e.Bankday);

            entity.ToTable("DateInfo", "padb");

            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
        });

    }

    private static void ConfigureInstrument(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.ToTable("Instrument", "padb");

            entity.HasIndex(e => e.InstrumentName, "UQ_Instrument_InstrumentName").IsUnique();

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentName).HasMaxLength(100);
            entity.Property(e => e.InstrumentTypeId).HasColumnName("InstrumentTypeID");

            entity.HasOne(d => d.InstrumentTypeNavigation).WithMany(p => p.InstrumentsNavigation)
                .HasForeignKey(d => d.InstrumentTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Instrument_InstrumentTypeID");
        });
    }
    private static void ConfigureInstrumentDayPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentDayPerformance>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.Bankday });

            entity.ToTable("InstrumentDayPerformance", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DayPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.InstrumentDayPerformancesNavigation)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentDayPerformance_Bankday");

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.InstrumentDayPerformancesNavigation)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentDayPerformance_InstrumentID");
        });
    }

    private static void ConfigureInstrumentHalfYearPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentHalfYearPerformance>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("InstrumentHalfYearPerformance", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HalfYearPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.InstrumentHalfYearPerformancesNavigation)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentHalfYearPerformance_InstrumentID");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.InstrumentHalfYearPerformancePeriodEndsNavigation)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentHalfYearPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.InstrumentHalfYearPerformancePeriodStartsNavigation)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentHalfYearPerformance_PeriodStart");
        });
    }

    private static void ConfigureInstrumentMonthPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentMonthPerformance>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("InstrumentMonthPerformance", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MonthPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.InstrumentMonthPerformancesNavigation)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentMonthPerformance_InstrumentID");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.InstrumentMonthPerformancePeriodEndsNavigation)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentMonthPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.InstrumentMonthPerformancePeriodStartsNavigation)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentMonthPerformance_PeriodStart");
        });
    }

    private static void ConfigureInstrumentPrice(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentPrice>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.Bankday });

            entity.ToTable("InstrumentPrice", "padb");

            entity.Property(e => e.InstrumentId).HasColumnName("InstrumentID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.InstrumentPricesNavigation)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InstrumentPrice_Bankday");

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.InstrumentPricesNavigation)
                .HasForeignKey(d => d.InstrumentId)
                .HasConstraintName("FK_InstrumentPrice_InstrumentID");
        });
    }

    private static void ConfigureInstrumentType(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentType>(entity =>
        {
            entity.ToTable("InstrumentType", "padb");

            entity.Property(e => e.InstrumentTypeId).HasColumnName("InstrumentTypeID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentTypeName).HasMaxLength(20);
        });
    }

    private static void ConfigureKeyFigureInfo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyFigureInfo>(entity =>
        {
            entity.HasKey(e => e.KeyFigureId);

            entity.ToTable("KeyFigureInfo", "padb");

            entity.HasIndex(e => e.KeyFigureName, "UQ_KeyFigureInfo_KeyFigureName").IsUnique();

            entity.Property(e => e.KeyFigureId).HasColumnName("KeyFigureID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.KeyFigureName).HasMaxLength(100);
        });
    }

    private static void ConfigureKeyFigureValue(ModelBuilder modelBuilder)
    {
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

            entity.HasOne(d => d.KeyFigureInfoNavigation).WithMany(p => p.KeyFigureValuesNavigation)
                .HasForeignKey(d => d.KeyFigureId)
                .HasConstraintName("FK_KeyFigureValue_KeyFigureID");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.KeyFigureValuesNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_KeyFigureValue_PortfolioID");
        });
    }

    private static void ConfigurePortfolio(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.ToTable("Portfolio", "padb");

            entity.HasIndex(e => e.PortfolioName, "UQ_Portfolio_PortfolioName").IsUnique();

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PortfolioName).HasMaxLength(100);

            entity.HasOne(p => p.User)
                .WithMany(u => u.PortfoliosNavigation)
                .HasForeignKey(p => p.UserID)
                .HasConstraintName("FK_Portfolio_UserID");

        });
    }

    private static void ConfigurePortfolioCumulativeDayPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioCumulativeDayPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.Bankday });

            entity.ToTable("PortfolioCumulativeDayPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CumulativeDayPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PortfolioCumulativeDayPerformancesNavigation)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioCumulativeDayPerformance_Bankday");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.PortfolioCumulativeDayPerformancesNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioCumulativeDayPerformance_PortfolioID");
        });
    }

    private static void ConfigurePortfolioDayPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioDayPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.Bankday });

            entity.ToTable("PortfolioDayPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DayPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PortfolioDayPerformancesNavigation)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioDayPerformance_Bankday");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.PortfolioDayPerformancesNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioDayPerformance_PortfolioID");
        });
    }

    private static void ConfigurePortfolioHalfYearPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioHalfYearPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("PortfolioHalfYearPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HalfYearPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.PortfolioHalfYearPerformancePeriodEndsNavigation)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioHalfYearPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.PortfolioHalfYearPerformancePeriodStartsNavigation)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioHalfYearPerformance_PeriodStart");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.PortfolioHalfYearPerformancesNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioHalfYearPerformance_PortfolioID");
        });
    }

    private static void ConfigurePortfolioMonthPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioMonthPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.PeriodStart, e.PeriodEnd });

            entity.ToTable("PortfolioMonthPerformance", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MonthPerformance).HasColumnType("decimal(24, 16)");

            entity.HasOne(d => d.PeriodEndNavigation).WithMany(p => p.PortfolioMonthPerformancePeriodEndsNavigation)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioMonthPerformance_PeriodEnd");

            entity.HasOne(d => d.PeriodStartNavigation).WithMany(p => p.PortfolioMonthPerformancePeriodStartsNavigation)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioMonthPerformance_PeriodStart");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.PortfolioMonthPerformancesNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioMonthPerformance_PortfolioID");
        });
    }

    private static void ConfigurePortfolioValue(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioValue>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.Bankday });

            entity.ToTable("PortfolioValue", "padb");

            entity.Property(e => e.PortfolioId).HasColumnName("PortfolioID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PortfolioValue1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PortfolioValue");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PortfolioValuesNavigation)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PortfolioValue_Bankday");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.PortfolioValuesNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("FK_PortfolioValue_PortfolioID");
        });
    }

    private static void ConfigurePosition(ModelBuilder modelBuilder)
    {
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

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PositionsNavigation)
                .HasForeignKey(d => d.Bankday)
                .HasConstraintName("FK_Position_Bankday");

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.PositionsNavigation)
                .HasForeignKey(d => d.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Position_InstrumentID");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.PositionsNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Position_PortfolioID");
        });
    }

    private static void ConfigurePositionValue(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PositionValue>(entity =>
        {
            entity.HasKey(e => new { e.PositionId, e.Bankday });

            entity.ToTable("PositionValue", "padb");

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PositionValue1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PositionValue");

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.PositionValuesNavigation)
                .HasForeignKey(d => d.Bankday)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PositionValue_Bankday");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.PositionValuesNavigation)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_PositionValue_PositionID");
        });
    }

    private static void ConfigureStaging(ModelBuilder modelBuilder)
    {
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
    }

    private static void ConfigureTransaction(ModelBuilder modelBuilder)
    {
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

            entity.HasOne(d => d.BankdayNavigation).WithMany(p => p.TransactionsNavigation)
                .HasForeignKey(d => d.Bankday)
                .HasConstraintName("FK_Transaction_Bankday");

            entity.HasOne(d => d.InstrumentNavigation).WithMany(p => p.TransactionsNavigation)
                .HasForeignKey(d => d.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Transaction_InstrumentID");

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.TransactionsNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Transaction_PortfolioID");

            entity.HasOne(d => d.TransactionTypeNavigation).WithMany(p => p.TransactionsNavigation)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Transaction_TransactionTypeID");
        });
    }

    private static void ConfigureTransactionType(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.ToTable("TransactionType", "padb");

            entity.HasIndex(e => e.TransactionTypeName, "UQ_TransactionType_TransactionTypeName").IsUnique();

            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");
            entity.Property(e => e.TransactionTypeName).HasMaxLength(20);
        });
    }


}