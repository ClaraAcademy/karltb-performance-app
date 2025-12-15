using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes;

namespace PerformanceApp.Infrastructure.Context.Configuration.Entities;

public static class KeyFigureInfoConfiguration
{
    public static void ConfigureKeyFigureInfo(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyFigureInfo>(Configure);
    }

    static void Configure(EntityTypeBuilder<KeyFigureInfo> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasIndex(e => e.Name, IndexKeyFigureInfo.Name)
            .IsUnique();
    }
}