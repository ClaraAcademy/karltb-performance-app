using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

public static class BenchmarkConfiguration
{
    public static void ConfigureBenchmark(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benchmark>(Configure);
    }

    static void Configure(EntityTypeBuilder<Benchmark> entity)
    {
        entity.HasKey(e => new { e.PortfolioId, e.BenchmarkId });

        entity.HasOne(d => d.BenchmarkPortfolioNavigation)
            .WithMany(p => p.BenchmarkBenchmarksNavigation)
            .HasForeignKey(d => d.BenchmarkId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(FkBenchmark.BenchmarkPortfolio);

        entity.HasOne(d => d.PortfolioPortfolioNavigation)
            .WithMany(p => p.BenchmarkPortfoliosNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(FkBenchmark.PortfolioPortfolio);
    }

}