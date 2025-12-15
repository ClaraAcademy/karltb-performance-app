using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Context.Configuration.Entities;

public static class InstrumentTypeConfiguration
{
    public static void ConfigureInstrumentType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstrumentType>(Configure);
    }

    static void Configure(EntityTypeBuilder<InstrumentType> entity)
    {
        entity.HasKey(e => e.Id);
    }

}