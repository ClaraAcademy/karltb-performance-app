using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Seeding;

public class BaseSeederTest : IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly PadbContext _context;
    protected readonly UserManager<ApplicationUser> _userManager;
    public BaseSeederTest(DatabaseFixture fixture)
    {
        _scope = fixture
            .ServiceProvider
            .CreateScope();
        _context = _scope
            .ServiceProvider
            .GetRequiredService<PadbContext>();
        _userManager = _scope
            .ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
    }

    public void Dispose()
    {
        _scope.Dispose();
        GC.SuppressFinalize(this);
    }
}