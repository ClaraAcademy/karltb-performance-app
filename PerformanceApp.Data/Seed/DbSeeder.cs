using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Seed;

public class Seeder(PadbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly PadbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly PasswordHasher<ApplicationUser> _passwordHasher = new();

    private async Task CreateUser(string username, string password)
        => await _userManager.CreateAsync(new ApplicationUser { UserName = username }, password);


    private bool UserExists(string username)
        => _context.Users.Any(
            u => u.UserName != null
                && u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)
        );

    private async Task SeedUsers()
    {
        var usernamePassword = new List<(string, string)>
        {
            ("User A", "Password A"),
            ("User B", "Password B")
        };

        foreach ((var username, var password) in usernamePassword)
        {
            if (UserExists(username)) { continue; }
            await CreateUser(username, password);
        }
        _context.SaveChanges();
    }

    public async Task Seed()
    {
        await SeedUsers();
    }

}