using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = KeyFigureInfoConstants;

public static class KeyFigureInfoConfiguration
{
    public static void ConfigureKeyFigureInfo(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyFigureInfo>(Configure);
    }

    static void Configure(EntityTypeBuilder<KeyFigureInfo> entity)
    {
        entity.HasKey(e => e.Id);

        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.HasIndex(e => e.Name, Constants.IndexName)
            .IsUnique();

        entity.Property(e => e.Id)
            .HasColumnName(Constants.IdColumnName);

        entity.Property(e => e.Created)
            .HasColumnName(Constants.CreatedColumnName  )
            .HasDefaultValueSql(Constants.CreatedDefaultValue);

        entity.Property(e => e.Name)
            .HasColumnName(Constants.NameColumnName);
    }
}