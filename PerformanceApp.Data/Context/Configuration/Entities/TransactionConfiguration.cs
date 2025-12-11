using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;

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
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Amount)
            .HasColumnType(Amount.SqlType);

        entity.Property(e => e.Nominal)
            .HasColumnType(Nominal.SqlType);

        entity.Property(e => e.Proportion)
            .HasColumnType(Proportion.SqlType);

        entity.HasOne(d => d.BankdayNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.Bankday)
            .HasConstraintName(FkTransaction.Bankday);

        entity.HasOne(d => d.InstrumentNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.InstrumentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(FkTransaction.InstrumentId);

        entity.HasOne(d => d.PortfolioNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(FkTransaction.PortfolioId);

        entity.HasOne(d => d.TransactionTypeNavigation)
            .WithMany(p => p.TransactionsNavigation)
            .HasForeignKey(d => d.TransactionTypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(FkTransaction.TransactionTypeId);
    }
}