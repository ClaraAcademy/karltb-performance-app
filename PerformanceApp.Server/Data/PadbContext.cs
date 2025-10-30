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
            // Set default schema
            modelBuilder.HasDefaultSchema("padb");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PerformanceApp.Server.Models.Portfolio> Portfolio { get; set; } = default!;
        public DbSet<PerformanceApp.Server.Models.Benchmark> Benchmark { get; set; } = default!;
    }
}
