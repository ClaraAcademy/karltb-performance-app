using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = TransactionTypeConstants;

public static class TransactionTypeConfiguration
{
    public static void ConfigureTransactionType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionType>(Configure);
    }

    static void Configure(EntityTypeBuilder<TransactionType> entity)
    {
        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.HasIndex(e => e.Name, Constants.IndexName)
            .IsUnique();

        entity.Property(e => e.Name)
            .HasMaxLength(20);
    }
}