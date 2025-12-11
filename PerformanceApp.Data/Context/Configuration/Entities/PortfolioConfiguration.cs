using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Entities;
using PerformanceApp.Data.Context.Configuration.Constants.Fks;

namespace PerformanceApp.Data.Context.Configuration.Entities;

using Constants = PortfolioConstants;

public static class PortfolioConfiguration
{
    public static void ConfigurePortfolio(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Portfolio>(Configure);
    }

    static void Configure(EntityTypeBuilder<Portfolio> entity)
    {
        entity.ToTable(Constants.TableName, Constants.DefaultSchema);

        entity.HasIndex(e => e.Name, Constants.IndexName)
            .IsUnique();

        entity.HasOne(p => p.User)
            .WithMany(u => u.PortfoliosNavigation)
            .HasForeignKey(p => p.UserID)
            .HasConstraintName(FkPortfolio.UserID);
    }

}