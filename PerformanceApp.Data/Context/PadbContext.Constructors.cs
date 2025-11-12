using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context;

public partial class PadbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public PadbContext() { }
    public PadbContext(DbContextOptions<PadbContext> options) : base(options) { }
}