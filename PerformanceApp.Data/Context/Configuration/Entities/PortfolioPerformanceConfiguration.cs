using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = PortfolioPerformanceConstants;

public static class PortfolioPerformanceConfiguration
{
    public static void ConfigurePortfolioPerformances(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioPerformance>(Configure);
    }
    static void Configure(EntityTypeBuilder<PortfolioPerformance> entity)
    {
        entity.HasKey(e => new { e.PortfolioId, e.PeriodStart, e.PeriodEnd, e.TypeId });

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.PortfolioId)
            .HasColumnName(Constants.PortfolioIdColumnName);

        entity.Property(e => e.TypeId)
            .HasColumnName(Constants.TypeIdColumnName);

        entity.Property(e => e.PeriodStart)
            .HasColumnName(Constants.PeriodStartColumnName);

        entity.Property(e => e.PeriodEnd)
            .HasColumnName(Constants.PeriodEndColumnName);

        entity.Property(e => e.Value)
            .HasColumnName(Constants.ValueColumnName)
            .HasColumnType(Constants.ValueColumnType);

        entity.Property(e => e.Created)
            .HasColumnName(Constants.CreatedColumnName)
            .HasDefaultValueSql(Constants.CreatedDefaultValue);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.PortfolioPerformancesNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.PortfolioIdForeignKeyName);

        entity.HasOne(d => d.PerformanceTypeNavigation)
            .WithMany(p => p.PortfolioPerformancesNavigation)
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.TypeIdForeignKeyName);

        entity.HasOne(d => d.PeriodStartNavigation)
            .WithMany(p => p.PortfolioPerformancesPeriodStartNavigation)
            .HasForeignKey(d => d.PeriodStart)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.PeriodStartForeignKeyName);

        entity.HasOne(d => d.PeriodEndNavigation)
            .WithMany(p => p.PortfolioPerformancesPeriodEndNavigation)
            .HasForeignKey(d => d.PeriodEnd)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.PeriodEndForeignKeyName);
    }
}