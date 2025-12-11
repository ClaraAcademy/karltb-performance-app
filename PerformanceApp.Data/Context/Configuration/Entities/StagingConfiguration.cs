using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = StagingConstants;

public static class StagingConfiguration
{
    public static void ConfigureStaging(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Staging>(Configure);
    }

    static void Configure(EntityTypeBuilder<Staging> entity)
    {
        entity.HasKey(e => new { e.Bankday, e.InstrumentName, e.InstrumentType });

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.Created)
            .HasColumnName(Constants.CreatedColumnName)
            .HasDefaultValueSql(Constants.CreatedDefaultValue);

        entity.Property(e => e.InstrumentName)
            .HasColumnName(Constants.InstrumentNameColumnName)
            .HasMaxLength(100);

        entity.Property(e => e.InstrumentType)
            .HasColumnName(Constants.InstrumentTypeColumnName)
            .HasMaxLength(100);

        entity.Property(e => e.Price)
            .HasColumnName(Constants.PriceColumnName)
            .HasColumnType(Constants.PriceColumnType);
    }
}