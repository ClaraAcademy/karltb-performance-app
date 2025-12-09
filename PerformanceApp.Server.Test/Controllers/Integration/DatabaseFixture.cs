using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class DatabaseFixture : IDisposable
{
    private static readonly string _connectionString
        = "Server=localhost\\SQLEXPRESS;Database=padb_integration_test;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
    private readonly ServiceProvider _serviceProvider;


    public DatabaseFixture()
    {
        _serviceProvider = new ServiceCollection()
            .AddDbContext<PadbContext>(options => options.UseSqlServer(_connectionString))
            .AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<PadbContext>()
            .Services
            .BuildServiceProvider();

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PadbContext>();
            context.Database.EnsureDeleted();
        }

        DatabaseInitializer
            .Initialize(_serviceProvider)
            .GetAwaiter()
            .GetResult();
    }
    public void Dispose()
    {
        using var context = _serviceProvider
            .GetRequiredService<PadbContext>();

        context.Database.EnsureDeleted();
        GC.SuppressFinalize(this);
    }
}