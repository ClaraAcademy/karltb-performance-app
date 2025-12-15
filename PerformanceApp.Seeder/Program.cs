using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data;
using PerformanceApp.Infrastructure.Context;

namespace PerformanceApp.Seeder;

public class Program
{
    static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();
        services.AddDataServices(configuration);

        var serviceProvider = services.BuildServiceProvider();

        /* BEGIN DELETE BEFORE SEEDING */
        using var scope = serviceProvider.CreateScope();
        var context = scope
            .ServiceProvider
            .GetRequiredService<PadbContext>();
        
        context.Database.EnsureDeleted();
        /* END DELETE BEFORE SEEDING */

        await DatabaseInitializer.Initialize(serviceProvider);
    }

}