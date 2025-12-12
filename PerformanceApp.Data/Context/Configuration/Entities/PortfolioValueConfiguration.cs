using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;

namespace PerformanceApp.Data.Context.Configuration.Entities;

public static class PortfolioValueConfiguration
{

    public static void ConfigurePortfolioValue(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioValue>(Configure);
    }

    static void Configure(EntityTypeBuilder<PortfolioValue> entity)
    {
        entity.HasKey(e => new { e.PortfolioId, e.Bankday });

        entity.Property(e => e.Value)
            .HasColumnType(Value.SqlType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.PortfolioValuesNavigation)
            .HasForeignKey(d => d.Bankday)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName(FkPortfolioValue.Bankday);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.PortfolioValuesNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .HasConstraintName(FkPortfolioValue.PortfolioId);
    }

}