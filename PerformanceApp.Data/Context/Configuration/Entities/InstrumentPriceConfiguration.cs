using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = InstrumentPriceConstants;

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