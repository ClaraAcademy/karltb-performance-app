using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = InstrumentPerformanceConstants;

public static class InstrumentPerformanceConfiguration
{
    public static void ConfigureInstrumentPerformance(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentPerformance>(Configure);
    }

    static void Configure(EntityTypeBuilder<InstrumentPerformance> entity)
    {
        entity.HasKey(e => new { e.InstrumentId, e.PeriodStart, e.PeriodEnd, e.TypeId });

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.InstrumentId)
            .HasColumnName(Constants.IdColumnName);

        entity.Property(e => e.TypeId)
            .HasColumnName(Constants.TypeIdColumnName);

        entity.Property(e => e.PeriodStart)
            .HasColumnName(Constants.PeriodStartColumnName);

        entity.Property(e => e.PeriodEnd)
            .HasColumnName(Constants.PeriodEndColumnName);

        entity.Property(e => e.Value)
            .HasColumnType(Constants.ValueColumnType)
            .HasColumnName(Constants.ValueColumnName);

        entity.Property(e => e.Created)
            .HasColumnName(Constants.CreatedColumnName)
            .HasDefaultValueSql(Constants.CreatedDefaultValue);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.InstrumentPerformancesNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.InstrumentForeignKeyName);

        entity.HasOne(d => d.PerformanceTypeNavigation)
            .WithMany(p => p.InstrumentPerformancesNavigation)
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.PerformanceTypeForeignKeyName);

        entity.HasOne(d => d.PeriodStartNavigation)
            .WithMany(p => p.InstrumentPerformancesPeriodStartNavigation)
            .HasForeignKey(d => d.PeriodStart)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.PeriodStartForeignKeyName);

        entity.HasOne(d => d.PeriodEndNavigation)
            .WithMany(p => p.InstrumentPerformancesPeriodEndNavigation)
            .HasForeignKey(d => d.PeriodEnd)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(Constants.PeriodEndForeignKeyName);
    }

}