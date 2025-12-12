using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

public static class DateInfoConfiguration
{
    public static void ConfigureDateInfo(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DateInfo>(Configure);
    }

    static void Configure(EntityTypeBuilder<DateInfo> entity)
    {
        entity.HasKey(e => e.Bankday);
    }
}