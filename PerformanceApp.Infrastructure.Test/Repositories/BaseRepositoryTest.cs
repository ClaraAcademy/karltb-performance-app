using Microsoft.EntityFrameworkCore;
using PerformanceApp.Infrastructure.Context;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class BaseRepositoryTest
{
    protected readonly PadbContext _context;

    public BaseRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<PadbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new PadbContext(options);
    }
}