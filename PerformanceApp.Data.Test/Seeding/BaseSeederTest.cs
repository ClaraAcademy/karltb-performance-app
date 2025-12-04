using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Seeding;

public class BaseSeederTest
{
    protected readonly IServiceScope _scope;
    protected readonly PadbContext _context;
    protected readonly UserManager<ApplicationUser> _userManager;

    private readonly DatabaseFixture _fixture;

    public BaseSeederTest(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture
            .ServiceProvider
            .CreateScope();
        _context = _scope
            .ServiceProvider
            .GetRequiredService<PadbContext>();
        _userManager = _scope
            .ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

}