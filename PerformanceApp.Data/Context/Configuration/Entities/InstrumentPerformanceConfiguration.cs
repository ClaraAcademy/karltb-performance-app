using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

public static class InstrumentPerformanceConfiguration
{
    public static void ConfigureInstrumentPerformance(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentPerformance>(Configure);
    }

    static void Configure(EntityTypeBuilder<InstrumentPerformance> entity)
    {
        entity.HasKey(e => new { e.InstrumentId, e.PeriodStart, e.PeriodEnd, e.TypeId });

        entity.Property(e => e.Value)
            .HasColumnType(Value.SqlType);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.InstrumentPerformancesNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(FkInstrumentPerformance.Instrument);

        entity.HasOne(d => d.PerformanceTypeNavigation)
            .WithMany(p => p.InstrumentPerformancesNavigation)
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(FkInstrumentPerformance.PerformanceType);

        entity.HasOne(d => d.PeriodStartNavigation)
            .WithMany(p => p.InstrumentPerformancesPeriodStartNavigation)
            .HasForeignKey(d => d.PeriodStart)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(FkInstrumentPerformance.PeriodStart);

        entity.HasOne(d => d.PeriodEndNavigation)
            .WithMany(p => p.InstrumentPerformancesPeriodEndNavigation)
            .HasForeignKey(d => d.PeriodEnd)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName(FkInstrumentPerformance.PeriodEnd);
    }

}