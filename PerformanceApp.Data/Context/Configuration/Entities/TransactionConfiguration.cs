using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = TransactionConstants;

public static class TransactionConfiguration
{
    public static void ConfigureTransaction(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(Configure);
    }

    static void Configure(EntityTypeBuilder<Transaction> entity)
    {
        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.Property(e => e.Amount)
            .HasColumnType(Constants.AmountType);

        entity.Property(e => e.Nominal)
            .HasColumnType(Constants.NominalType);

        entity.Property(e => e.Proportion)
            .HasColumnType(Constants.ProportionType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.Bankday)
            .HasConstraintName(Constants.BankdayForeignKeyName);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(Constants.InstrumentForeignKeyName);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(Constants.PortfolioForeignKeyName);

        entity.HasOne(d => d.TransactionTypeNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.TransactionTypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(Constants.TransactionTypeForeignKeyName);
    }
}