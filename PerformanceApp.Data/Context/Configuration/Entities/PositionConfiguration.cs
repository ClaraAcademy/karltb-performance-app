using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = PositionConstants;

public static class PositionConfiguration
{
    public static void ConfigurePosition(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Position>(Configure);
    }
    static void Configure(EntityTypeBuilder<Position> entity)
    {
        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.Amount)
            .HasColumnType(Amount.SqlType);

        entity.Property(e => e.Nominal)
            .HasColumnType(Nominal.SqlType);

        entity.Property(e => e.Proportion)
            .HasColumnType(Proportion.SqlType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.PositionsNavigation)
            .HasForeignKey(d => d.Bankday)
            .HasConstraintName(Constants.BankdayForeignKeyName);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.PositionsNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(Constants.InstrumentIdForeignKeyName);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.PositionsNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(Constants.PortfolioIdForeignKeyName);
    }

}