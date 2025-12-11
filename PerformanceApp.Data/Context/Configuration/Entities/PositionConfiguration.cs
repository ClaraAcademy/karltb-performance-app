using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;

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

        entity.Property(e => e.Id)
            .HasColumnName(Constants.IdColumnName);

        entity.Property(e => e.Amount)
            .HasColumnName(Constants.AmountColumnName)
            .HasColumnType(Constants.AmountType);

        entity.Property(e => e.Created)
            .HasColumnName(Constants.CreatedColumnName)
            .HasDefaultValueSql(Constants.CreatedDefaultValue);

        entity.Property(e => e.InstrumentId)
            .HasColumnName(Constants.InstrumentIdColumnName);

        entity.Property(e => e.Nominal)
            .HasColumnName(Constants.NominalColumnName)
            .HasColumnType(Constants.NominalType);

        entity.Property(e => e.PortfolioId)
            .HasColumnName(Constants.PortfolioIdColumnName);

        entity.Property(e => e.Proportion)
            .HasColumnName(Constants.ProportionColumnName)
            .HasColumnType(Constants.ProportionType);

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