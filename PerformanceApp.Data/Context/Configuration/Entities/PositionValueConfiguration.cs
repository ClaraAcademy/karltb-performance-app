using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;

namespace PerformanceApp.Data.Context.Configuration.Entities;

public static class PositionValueConfiguration
{

    public static void ConfigurePositionValue(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PositionValue>(Configure);
    }
    private static void Configure(EntityTypeBuilder<PositionValue> entity)
    {
        entity.HasKey(e => new { e.PositionId, e.Bankday });

        entity.Property(e => e.Value)
            .HasColumnType(Value.SqlType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.PositionValuesNavigation)
            .HasForeignKey(d => d.Bankday)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName(FkPositionValue.Bankday);

        entity.HasOne(d => d.PositionNavigation)
            .WithMany(p => p.PositionValuesNavigation)
            .HasForeignKey(d => d.PositionId)
            .HasConstraintName(FkPositionValue.PositionId);
    }
}