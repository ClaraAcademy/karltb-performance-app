using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Test.Repositories;

public class RepositoryTest
{
    protected static PadbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<PadbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PadbContext(options);
    }
}