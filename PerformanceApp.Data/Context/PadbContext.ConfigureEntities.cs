using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context;

public partial class PadbContext
{
    private static void ConfigureBenchmark(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benchmark>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.BenchmarkId });

            entity.ToTable("Benchmark", "padb");

            entity.Property(e => e.PortfolioId)
                .HasColumnName("PortfolioID");
            entity.Property(e => e.BenchmarkId)
                .HasColumnName("BenchmarkID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");

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

            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
        });

    }

    private static void ConfigureInstrument(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.ToTable("Instrument", "padb");

            entity.HasIndex(e => e.Name, "UQ_Instrument_InstrumentName").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("InstrumentID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .HasColumnName("InstrumentName")
                .HasMaxLength(100);
            entity.Property(e => e.TypeId)
                .HasColumnName("InstrumentTypeID");

            entity.HasOne(d => d.InstrumentTypeNavigation).WithMany(p => p.InstrumentsNavigation)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Instrument_InstrumentTypeID");
        });
    }

    private static void ConfigureInstrumentPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentPerformance>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.PeriodStart, e.PeriodEnd, e.TypeId });

            entity.ToTable("InstrumentPerformance", "padb");

            entity.Property(e => e.InstrumentId)
                .HasColumnName("InstrumentID");
            entity.Property(e => e.TypeId)
                .HasColumnName("TypeID");
            entity.Property(e => e.PeriodStart)
                .HasColumnName("PeriodStart");
            entity.Property(e => e.PeriodEnd)
                .HasColumnName("PeriodEnd");
            entity.Property(e => e.Value)
                .HasColumnType("decimal(24, 16)")
                .HasColumnName("Value");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created");

            entity.HasOne(d => d.InstrumentNavigation)
                .WithMany(p => p.InstrumentPerformancesNavigation)
                .HasForeignKey(d => d.InstrumentId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_InstrumentPerformance_InstrumentID");
            entity.HasOne(d => d.PerformanceTypeNavigation)
                .WithMany(p => p.InstrumentPerformancesNavigation)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_InstrumentPerformance_TypeID");
            entity.HasOne(d => d.PeriodStartNavigation)
                .WithMany(p => p.InstrumentPerformancesPeriodStartNavigation)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_InstrumentPerformance_PeriodStart");
            entity.HasOne(d => d.PeriodEndNavigation)
                .WithMany(p => p.InstrumentPerformancesPeriodEndNavigation)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_InstrumentPerformance_PeriodEnd");
        });

    }

    private static void ConfigureInstrumentPrice(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentPrice>(entity =>
        {
            entity.HasKey(e => new { e.InstrumentId, e.Bankday });

            entity.ToTable("InstrumentPrice", "padb");

            entity.Property(e => e.InstrumentId)
                .HasColumnName("InstrumentID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price)
                .HasColumnName("Price")
                .HasColumnType("decimal(19, 4)");

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

            entity.Property(e => e.Id)
                .HasColumnName("InstrumentTypeID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .HasColumnName("InstrumentTypeName")
                .HasMaxLength(20);
        });
    }

    private static void ConfigureKeyFigureInfo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyFigureInfo>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("KeyFigureInfo", "padb");

            entity.HasIndex(e => e.Name, "UQ_KeyFigureInfo_KeyFigureName").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("KeyFigureID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .HasColumnName("KeyFigureName")
                .HasMaxLength(100);
        });
    }

    private static void ConfigureKeyFigureValue(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyFigureValue>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.KeyFigureId });

            entity.ToTable("KeyFigureValue", "padb");

            entity.Property(e => e.PortfolioId)
                .HasColumnName("PortfolioID");
            entity.Property(e => e.KeyFigureId)
                .HasColumnName("KeyFigureID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Value)
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
    private static void ConfigurePerformanceTypeInfo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PerformanceType>(entity =>
        {
            entity.ToTable("PerformanceTypeInfo", "padb");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Name, "UQ_PerformanceTypeInfo_Name").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasColumnName("Name");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
        });
    }

    private static void ConfigurePortfolio(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.ToTable("Portfolio", "padb");

            entity.HasIndex(e => e.Name, "UQ_Portfolio_PortfolioName").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("PortfolioID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .HasColumnName("PortfolioName")
                .HasMaxLength(100);

            entity.HasOne(p => p.User)
                .WithMany(u => u.PortfoliosNavigation)
                .HasForeignKey(p => p.UserID)
                .HasConstraintName("FK_Portfolio_UserID");

        });
    }

    private static void ConfigurePortfolioPerformance(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioPerformance>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.PeriodStart, e.PeriodEnd, e.TypeId });

            entity.ToTable("PortfolioPerformance", "padb");

            entity.Property(e => e.PortfolioId)
                .HasColumnName("PortfolioID");
            entity.Property(e => e.TypeId)
                .HasColumnName("TypeID");
            entity.Property(e => e.PeriodStart)
                .HasColumnName("PeriodStart");
            entity.Property(e => e.PeriodEnd)
                .HasColumnName("PeriodEnd");
            entity.Property(e => e.Value)
                .HasColumnType("decimal(24, 16)")
                .HasColumnName("Value");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created");

            entity.HasOne(d => d.PortfolioNavigation)
                .WithMany(p => p.PortfolioPerformancesNavigation)
                .HasForeignKey(d => d.PortfolioId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PortfolioPerformance_PortfolioID");
            entity.HasOne(d => d.PerformanceTypeNavigation)
                .WithMany(p => p.PortfolioPerformancesNavigation)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PortfolioPerformance_TypeID");
            entity.HasOne(d => d.PeriodStartNavigation)
                .WithMany(p => p.PortfolioPerformancesPeriodStartNavigation)
                .HasForeignKey(d => d.PeriodStart)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PortfolioPerformance_PeriodStart");
            entity.HasOne(d => d.PeriodEndNavigation)
                .WithMany(p => p.PortfolioPerformancesPeriodEndNavigation)
                .HasForeignKey(d => d.PeriodEnd)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PortfolioPerformance_PeriodEnd");
        });
    }

    private static void ConfigurePortfolioValue(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioValue>(entity =>
        {
            entity.HasKey(e => new { e.PortfolioId, e.Bankday });

            entity.ToTable("PortfolioValue", "padb");

            entity.Property(e => e.PortfolioId)
                .HasColumnName("PortfolioID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Value)
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

            entity.Property(e => e.Id)
                .HasColumnName("PositionID");
            entity.Property(e => e.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentId)
                .HasColumnName("InstrumentID");
            entity.Property(e => e.Nominal)
                .HasColumnName("Nominal")
                .HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PortfolioId)
                .HasColumnName("PortfolioID");
            entity.Property(e => e.Proportion)
                .HasColumnName("Proportion")
                .HasColumnType("decimal(5, 4)");

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

            entity.Property(e => e.PositionId)
                .HasColumnName("PositionID");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Value)
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
            entity.HasKey(e => new { e.Bankday, e.InstrumentName, e.InstrumentType });
            entity.ToTable("Staging", "padb");

            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentName)
                .HasColumnName("InstrumentName")
                .HasMaxLength(100);
            entity.Property(e => e.InstrumentType)
                .HasColumnName("InstrumentType")
                .HasMaxLength(100);
            entity.Property(e => e.Price)
                .HasColumnName("Price")
                .HasColumnType("decimal(19, 4)");
        });
    }

    private static void ConfigureTransaction(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction", "padb");

            entity.Property(e => e.Id)
                .HasColumnName("TransactionID");
            entity.Property(e => e.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Created)
                .HasColumnName("Created")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InstrumentId)
                .HasColumnName("InstrumentID");
            entity.Property(e => e.Nominal)
                .HasColumnName("Nominal")
                .HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PortfolioId)
                .HasColumnName("PortfolioID");
            entity.Property(e => e.Proportion)
                .HasColumnName("Proportion")
                .HasColumnType("decimal(5, 4)");
            entity.Property(e => e.TransactionTypeId)
                .HasColumnName("TransactionTypeID");

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

            entity.HasIndex(e => e.Name, "UQ_TransactionType_TransactionTypeName").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("TransactionTypeID");
            entity.Property(e => e.Name)
                .HasColumnName("TransactionTypeName")
                .HasMaxLength(20);
        });
    }


}