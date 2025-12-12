using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

public static class KeyFigureValueConfiguration
{
    public static void ConfigureKeyFigureValue(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyFigureValue>(Configure);
    }

    static void Configure(EntityTypeBuilder<KeyFigureValue> entity)
    {
        entity.HasKey(e => new { e.PortfolioId, e.KeyFigureId });

        entity.Property(e => e.Value)
            .HasColumnType(Value.SqlType);

        entity.HasOne(d => d.KeyFigureInfoNavigation)
            .WithMany(p => p.KeyFigureValuesNavigation)
            .HasForeignKey(d => d.KeyFigureId)
            .HasConstraintName(FkKeyFigureValue.KeyFigureId);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.KeyFigureValuesNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .HasConstraintName(FkKeyFigureValue.PortfolioId);
    }

}