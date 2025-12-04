using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Seeding;

public class BaseSeederTest : IDisposable
{
    protected readonly IServiceScope _scope;
    protected readonly PadbContext _context;
    protected readonly UserManager<ApplicationUser> _userManager;

    public BaseSeederTest()
    {
        var services = GetServiceProvider();
        _scope = services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<PadbContext>();
        _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _scope.Dispose();
        GC.SuppressFinalize(this);
    }
    private static IServiceProvider GetServiceProvider()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = config.GetConnectionString("TestContext");

        var services = new ServiceCollection();
        services.AddDbContext<PadbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<PadbContext>();

        return services.BuildServiceProvider();
    }
}