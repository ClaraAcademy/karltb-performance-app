using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Test.Seeding;

using PerformanceApp.Data.Seeding.Entities;
using Xunit;
using System.Threading.Tasks;
using PerformanceApp.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class DatabaseSeederTest
{
    private readonly PadbContext _context;
    private readonly DatabaseSeeder _seeder;
    private readonly UserManager<ApplicationUser> _userManager;

    public static UserManager<ApplicationUser> GetInMemoryUserManager(PadbContext context)
    {
        var store = new UserStore<ApplicationUser>(context);
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var userManager = new UserManager<ApplicationUser>(
            store,
            null, // IOptions<IdentityOptions> optionsAccessor
            new PasswordHasher<ApplicationUser>(),
            [],
            [],
            null, // ILookupNormalizer keyNormalizer
            null, // IdentityErrorDescriber errors
            null, // IServiceProvider services
            null  // ILogger<UserManager<ApplicationUser>> logger
        );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        return userManager;
    }

    public DatabaseSeederTest()
    {
        var options = new DbContextOptionsBuilder<PadbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new PadbContext(options);
        _userManager = GetInMemoryUserManager(_context);
        _seeder = new DatabaseSeeder(_context, _userManager);
    }

    public async Task FullDatabaseSeeding_PopulatesAllTables()
    {
        // Act
        await _seeder.Seed();

        // Assert
        Assert.NotEmpty(_context.Benchmarks);
        Assert.NotEmpty(_context.DateInfos);
        Assert.NotEmpty(_context.Instruments);
        Assert.NotEmpty(_context.InstrumentPerformances);
        Assert.NotEmpty(_context.InstrumentPrices);
        Assert.NotEmpty(_context.InstrumentTypes);
        Assert.NotEmpty(_context.KeyFigureInfos);
        Assert.NotEmpty(_context.KeyFigureValues);
        Assert.NotEmpty(_context.Portfolios);
        Assert.NotEmpty(_context.PortfolioPerformances);
        Assert.NotEmpty(_context.PortfolioValues);
        Assert.NotEmpty(_context.Positions);
        Assert.NotEmpty(_context.PositionValues);
        Assert.NotEmpty(_context.Stagings);
        Assert.NotEmpty(_context.Transactions);
        Assert.NotEmpty(_context.TransactionTypes);
        Assert.NotEmpty(_context.PerformanceTypeInfos);
    }

}