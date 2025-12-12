using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Indexes;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

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