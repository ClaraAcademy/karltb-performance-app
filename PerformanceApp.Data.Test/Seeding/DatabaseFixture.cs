using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Seeding;

public class DatabaseFixture : IDisposable
{
    private static readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=padb_test;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
    protected readonly ServiceProvider _serviceProvider;
    public ServiceProvider ServiceProvider => _serviceProvider;

    public DatabaseFixture()
    {
        _serviceProvider = new ServiceCollection()
            .AddDbContext<PadbContext>(options => options.UseSqlServer(_connectionString))
            .AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<PadbContext>()
            .Services
            .BuildServiceProvider();

    }
    public void Dispose()
    {
        using var context = _serviceProvider.GetRequiredService<PadbContext>();

        context.Database.EnsureDeleted();
        GC.SuppressFinalize(this);
    }
}