using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class DataBaseFixture : IDisposable
{
    private static readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=padb_test;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
    private readonly ServiceProvider _serviceProvider;


    public DataBaseFixture()
    {
        _serviceProvider = new ServiceCollection()
            .AddDbContext<PadbContext>(options => options.UseSqlServer(_connectionString))
            .AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<PadbContext>()
            .Services
            .BuildServiceProvider();

        DatabaseInitializer.Initialize(_serviceProvider).GetAwaiter().GetResult();
    }
    public void Dispose()
    {
        using var context = _serviceProvider.GetRequiredService<PadbContext>();

        context.Database.EnsureDeleted();
        GC.SuppressFinalize(this);
    }
}