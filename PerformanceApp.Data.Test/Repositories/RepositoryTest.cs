using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Test.Repositories;

public static class RepositoryTest
{
    public static PadbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<PadbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PadbContext(options);
    }
}