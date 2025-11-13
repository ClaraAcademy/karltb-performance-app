using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seed;
using System.IO;
using System.Threading.Tasks;

namespace PerformanceApp.Data;

public class Program
{
    private const string ContextName = "PadbContext";
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var services = new ServiceCollection();

        services.AddDataServices(configuration);

        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<PadbContext>();

            context.Database.EnsureCreated();
            SqlExecutor.ExecuteFilesInDirectory(context, SqlPaths.Functions).GetAwaiter();
            SqlExecutor.ExecuteFilesInDirectory(context, SqlPaths.StoredProcedures).GetAwaiter();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            new Seeder(context, userManager).Seed();
        }


    }

}
