using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = PortfolioValueConstants;

public static class PortfolioValueConfiguration
{

    public static void ConfigurePortfolioValue(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioValue>(Configure);
    }

    static void Configure(EntityTypeBuilder<PortfolioValue> entity)
    {
        entity.HasKey(e => new { e.PortfolioId, e.Bankday });

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.Value)
            .HasColumnType(Constants.ValueColumnType);

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