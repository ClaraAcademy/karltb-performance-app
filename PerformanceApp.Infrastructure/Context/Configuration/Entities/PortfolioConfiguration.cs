using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

namespace PerformanceApp.Infrastructure.Context.Configuration.Entities;

public static class PortfolioConfiguration
{
    public static void ConfigurePortfolio(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Portfolio>(Configure);
    }

    static void Configure(EntityTypeBuilder<Portfolio> entity)
    {
        entity.HasKey(e => e.Id);

        entity.HasIndex(e => e.Name, IndexPortfolio.Name)
            .IsUnique();

        entity.HasOne(p => p.User)
            .WithMany(u => u.PortfoliosNavigation)
            .HasForeignKey(p => p.UserID)
            .HasConstraintName(FkPortfolio.UserID);

        // Ignore shorthand navigations
        entity.Ignore(e => e.BenchmarksNavigation);
        entity.Ignore(e => e.PortfoliosNavigation);
    }

}