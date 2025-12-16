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
            .UseInMemoryDatabase(databaseName: name)
            .Options;
        _context = new PadbContext(options);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}