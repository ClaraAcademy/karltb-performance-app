using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Server.Data;

public partial class PadbContext : DbContext
{
    public PadbContext() { }
    public PadbContext(DbContextOptions<PadbContext> options) : base(options) { }
}