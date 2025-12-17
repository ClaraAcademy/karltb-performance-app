using Microsoft.EntityFrameworkCore;
using PerformanceApp.Infrastructure.Context;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class BaseRepositoryTest : IDisposable
{
    protected readonly PadbContext _context;

    public BaseRepositoryTest()
    {
        var name = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<PadbContext>()
            .UseInMemoryDatabase(name)
            .EnableSensitiveDataLogging(true) // TODO: Remove in production
            .Options;

        _context = new PadbContext(options);
        _context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}