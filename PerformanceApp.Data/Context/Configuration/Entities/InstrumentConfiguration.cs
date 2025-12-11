using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = InstrumentConstants;

public static class InstrumentConfiguration
{
    public static void ConfigureInstrument(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instrument>(Configure);
    }

    static void Configure(EntityTypeBuilder<Instrument> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasOne(d => d.InstrumentTypeNavigation)
            .WithMany(p => p.InstrumentsNavigation)
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(FkInstrument.InstrumentType);
    }
}