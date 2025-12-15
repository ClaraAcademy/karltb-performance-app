using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Seeder;

public static class DatabaseInitializer
{
    public static async Task Initialize(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PadbContext>();

        context.Database.EnsureCreated();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var seeder = new DatabaseSeeder(context, userManager);
        await seeder.Seed();
    }

}