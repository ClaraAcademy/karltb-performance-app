using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context;

public partial class PadbContext
{
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:PadbContext");
        */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Initialize Identity
        base.OnModelCreating(modelBuilder);

        // Initialize models
        ConfigureBenchmark(modelBuilder);
        ConfigurePerformanceTypeInfo(modelBuilder);
        ConfigureDateInfo(modelBuilder);
        ConfigureInstrument(modelBuilder);
        ConfigureInstrumentPerformance(modelBuilder);
        ConfigureInstrumentPrice(modelBuilder);
        ConfigureInstrumentType(modelBuilder);
        ConfigureKeyFigureInfo(modelBuilder);
        ConfigureKeyFigureValue(modelBuilder);
        ConfigurePortfolio(modelBuilder);
        ConfigurePortfolioPerformance(modelBuilder);
        ConfigurePortfolioValue(modelBuilder);
        ConfigurePosition(modelBuilder);
        ConfigurePositionValue(modelBuilder);
        ConfigureStaging(modelBuilder);
        ConfigureTransaction(modelBuilder);
        ConfigureTransactionType(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
