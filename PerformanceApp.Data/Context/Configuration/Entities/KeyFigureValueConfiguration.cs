using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = KeyFigureValueConstants;

public static class KeyFigureValueConfiguration
{
    public static void ConfigureKeyFigureValue(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyFigureValue>(Configure);
    }

    static void Configure(EntityTypeBuilder<KeyFigureValue> entity)
    {
        entity.HasKey(e => new { e.PortfolioId, e.KeyFigureId });

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.PortfolioId)
            .HasColumnName(Constants.PortfolioIdColumnName);

        entity.Property(e => e.KeyFigureId)
            .HasColumnName(Constants.KeyFigureIdColumnName);

        entity.Property(e => e.Created)
            .HasColumnName(Constants.CreatedColumnName)
            .HasDefaultValueSql(Constants.CreatedDefaultValue);

        entity.Property(e => e.Value)
            .HasColumnType(Constants.ValueColumnType)
            .HasColumnName(Constants.ValueColumnName);

        entity.HasOne(d => d.KeyFigureInfoNavigation)
            .WithMany(p => p.KeyFigureValuesNavigation)
            .HasForeignKey(d => d.KeyFigureId)
            .HasConstraintName(Constants.KeyFigureIdForeignKeyName);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.KeyFigureValuesNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .HasConstraintName(Constants.PortfolioIdForeignKeyName);
    }

}