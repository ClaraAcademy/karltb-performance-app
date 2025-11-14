using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Seeding;

public class UserSeeder(UserManager<ApplicationUser> userManager)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private static ApplicationUser ToUser(string username) => new() { UserName = username };

    private bool UserExists(string username) => _userManager.FindByNameAsync(username).Result != null;

    public void Seed()
    {
        var usernamesPasswords = new List<(string, string)>
        {
            ("UserA", "Password123!"),
            ("UserB", "Password123!")
        };

        foreach ((var username, var password) in usernamesPasswords)
        {
            if (!UserExists(username))
            {
                var result = _userManager.CreateAsync(ToUser(username), password).Result;
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create user {username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}