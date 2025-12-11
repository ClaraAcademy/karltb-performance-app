using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = BenchmarkConstants;

public static class BenchmarkConfiguration
{
    public static void ConfigureBenchmark(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benchmark>(Configure);
    }

    static void Configure(EntityTypeBuilder<Benchmark> entity)
    {
        entity.HasKey(e => new { e.PortfolioId, e.BenchmarkId });

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.HasOne(d => d.BenchmarkPortfolioNavigation)
            .WithMany(p => p.BenchmarkBenchmarksNavigation)
            .HasForeignKey(d => d.BenchmarkId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName(Constants.BenchmarkPortfolioForeignKey);

        entity.HasOne(d => d.PortfolioPortfolioNavigation)
            .WithMany(p => p.BenchmarkPortfoliosNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .HasConstraintName(Constants.PortfolioPortfolioForeignKey);

    }

}