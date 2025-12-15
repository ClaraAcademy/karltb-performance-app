using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Columns;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

namespace PerformanceApp.Infrastructure.Context.Configuration.Entities;

public static class PositionConfiguration
{
    public static void ConfigurePosition(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Position>(Configure);
    }
    static void Configure(EntityTypeBuilder<Position> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Amount)
            .HasColumnType(Amount.SqlType);

        entity.Property(e => e.Nominal)
            .HasColumnType(Nominal.SqlType);

        entity.Property(e => e.Proportion)
            .HasColumnType(Proportion.SqlType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.PositionsNavigation)
            .HasForeignKey(d => d.Bankday)
            .HasConstraintName(FkPosition.Bankday);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.PositionsNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(FkPosition.InstrumentId);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.PositionsNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(FkPosition.PortfolioId);
    }

}