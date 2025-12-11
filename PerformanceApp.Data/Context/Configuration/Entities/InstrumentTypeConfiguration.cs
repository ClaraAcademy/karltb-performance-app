using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = InstrumentTypeConstants;

public static class InstrumentTypeConfiguration
{
    public static void ConfigureInstrumentType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentType>(Configure);
    }

    static void Configure(EntityTypeBuilder<InstrumentType> entity)
    {
        entity.ToTable(Constants.TableName, Constants.DefaultSchema);
    }

}