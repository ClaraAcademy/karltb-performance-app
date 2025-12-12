using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Indexes;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

public static class PerformanceTypeConfiguration
{
    public static void ConfigurePerformanceType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PerformanceType>(Configure);
    }

    private static void Configure(EntityTypeBuilder<PerformanceType> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasIndex(e => e.Name, IndexPerformanceType.Name)
            .IsUnique();
    }

}