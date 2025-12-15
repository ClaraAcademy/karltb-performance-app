using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes;

namespace PerformanceApp.Infrastructure.Context.Configuration.Entities;

public static class TransactionTypeConfiguration
{
    public static void ConfigureTransactionType(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionType>(Configure);
    }

    static void Configure(EntityTypeBuilder<TransactionType> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasIndex(e => e.Name, IndexTransactionType.Name)
            .IsUnique();

        entity.Property(e => e.Name)
            .HasMaxLength(20);
    }
}