using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
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
        entity.HasIndex(e => e.Name, Constants.IndexName)
            .IsUnique();

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.Created)
            .HasColumnName(Constants.CreatedColumnName)
            .HasDefaultValueSql(Constants.CreatedDefaultValue);

        entity.HasOne(d => d.InstrumentTypeNavigation)
            .WithMany(p => p.InstrumentsNavigation)
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(Constants.InstrumentTypeForeignKeyName);

    }
}