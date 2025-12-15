using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Columns;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

namespace PerformanceApp.Infrastructure.Context.Configuration.Entities;

public static class InstrumentPriceConfiguration
{
    public static void ConfigureInstrumentPrice(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentPrice>(Configure);
    }

    static void Configure(EntityTypeBuilder<InstrumentPrice> entity)
    {
        entity.HasKey(e => new { e.InstrumentId, e.Bankday });

        entity.Property(e => e.Price)
            .HasColumnType(Price.SqlType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.InstrumentPricesNavigation)
            .HasForeignKey(d => d.Bankday)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName(FkInstrumentPrice.Bankday);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.InstrumentPricesNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .HasConstraintName(FkInstrumentPrice.Instrument);
    }
}