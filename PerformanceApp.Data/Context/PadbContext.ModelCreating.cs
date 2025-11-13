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
        ConfigureDateInfo(modelBuilder);
        ConfigureInstrument(modelBuilder);
        ConfigureInstrumentDayPerformance(modelBuilder);
        ConfigureInstrumentHalfYearPerformance(modelBuilder);
        ConfigureInstrumentMonthPerformance(modelBuilder);
        ConfigureInstrumentPrice(modelBuilder);
        ConfigureInstrumentType(modelBuilder);
        ConfigureKeyFigureInfo(modelBuilder);
        ConfigureKeyFigureValue(modelBuilder);
        ConfigurePortfolio(modelBuilder);
        ConfigurePortfolioCumulativeDayPerformance(modelBuilder);
        ConfigurePortfolioDayPerformance(modelBuilder);
        ConfigurePortfolioHalfYearPerformance(modelBuilder);
        ConfigurePortfolioMonthPerformance(modelBuilder);
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
