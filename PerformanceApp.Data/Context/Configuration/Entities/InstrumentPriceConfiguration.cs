using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
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

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.Price)
            .HasColumnType(Constants.PriceColumnType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.InstrumentPricesNavigation)
            .HasForeignKey(d => d.Bankday)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName(Constants.BankdayForeignKeyName);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.InstrumentPricesNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .HasConstraintName(Constants.InstrumentForeignKeyName);
    }
}