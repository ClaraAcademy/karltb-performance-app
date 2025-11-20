using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Test.Repositories;

public class BaseRepositoryTest
{
    protected readonly PadbContext _context;

    public BaseRepositoryTest()
    {
        _context = GetContext();
    }

    public static PadbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<PadbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PadbContext(options);
    }
}