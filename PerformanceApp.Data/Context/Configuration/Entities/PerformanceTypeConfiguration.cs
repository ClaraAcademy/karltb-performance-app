using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = PerformanceTypeConstants;

public static class PerformanceTypeConfiguration
{
    public static void ConfigurePerformanceType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PerformanceType>(Configure);
    }

    private static void Configure(EntityTypeBuilder<PerformanceType> entity)
    {
        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.HasKey(e => e.Id);

        entity.HasIndex(e => e.Name, Constants.IndexName)
            .IsUnique();

        entity.Property(e => e.Created)
            .HasDefaultValueSql(Constants.CreatedDefaultValue);
    }

}