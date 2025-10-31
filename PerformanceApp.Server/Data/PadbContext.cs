using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.Models;

namespace PerformanceApp.Server.Data
{
    public class PadbContext : DbContext
    {
        public PadbContext(DbContextOptions<PadbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set default Schema
            modelBuilder.HasDefaultSchema("padb");
            base.OnModelCreating(modelBuilder);

            // Configure models

            modelBuilder.Entity<DateInfo>(entity =>
            {
                entity.HasKey(di => di.Bankday);

                entity.HasMany(di => di.InstrumentPrices)
                    .WithOne(ip => ip.DateInfo);
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(p => p.PortfolioID);
            });

            modelBuilder.Entity<Benchmark>(entity =>
            {
                entity.HasKey(b => new { b.PortfolioID, b.BenchmarkID });

                entity.HasOne(b => b.BenchmarkPortfolio)
                    .WithMany()
                    .HasForeignKey(b => b.BenchmarkID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Portfolio)
                    .WithMany()
                    .HasForeignKey(b => b.PortfolioID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InstrumentType>(entity =>
            {
                entity.HasKey(it => it.InstrumentTypeID);

                entity.HasMany(it => it.Instruments)
                    .WithOne(i => i.InstrumentType);
            });

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.HasKey(i => i.InstrumentID);

                entity.HasOne(i => i.InstrumentType)
                    .WithMany(it => it.Instruments);

                entity.HasMany(i => i.InstrumentPrices)
                    .WithOne(ip => ip.Instrument);
            });

            modelBuilder.Entity<InstrumentPrice>(entity =>
            {
                entity.HasKey(ip => new { ip.InstrumentID, ip.Bankday });

                entity.HasOne(ip => ip.Instrument)
                    .WithMany(i => i.InstrumentPrices);

            });

        }

        public DbSet<Portfolio> Portfolio { get; set; } = default!;
        public DbSet<Benchmark> Benchmark { get; set; } = default!;
        public DbSet<Position> Position { get; set; }
        public DbSet<InstrumentType> InstrumentTypes { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<DateInfo> DateInfo { get; set; }
    }
}
