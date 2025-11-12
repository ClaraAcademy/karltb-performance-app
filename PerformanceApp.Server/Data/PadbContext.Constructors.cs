using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Server.Models;

namespace PerformanceApp.Server.Data;

public partial class PadbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public PadbContext() { }
    public PadbContext(DbContextOptions<PadbContext> options) : base(options) { }
}