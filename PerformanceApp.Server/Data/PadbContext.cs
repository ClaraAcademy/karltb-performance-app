using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.Models;

namespace PerformanceApp.Server.Data;

public partial class PadbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
