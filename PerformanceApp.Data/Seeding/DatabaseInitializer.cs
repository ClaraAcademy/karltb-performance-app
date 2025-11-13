using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Seeding;

public static class DatabaseInitializer
{
    public static void Initialize(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PadbContext>();

        context.Database.EnsureCreated();

        SqlExecutor.ExecuteFilesInDirectory(context, SqlPaths.Functions).GetAwaiter();
        SqlExecutor.ExecuteFilesInDirectory(context, SqlPaths.StoredProcedures).GetAwaiter();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var seeder = new DatabaseSeeder(context, userManager);
        seeder.Seed();
    }

}