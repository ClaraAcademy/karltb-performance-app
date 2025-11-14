using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Entities;

public class UserSeeder(UserManager<ApplicationUser> userManager)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private static ApplicationUser MapToUser(string username) => new() { UserName = username };

    private record Dto(string Username, string Password);

    private async Task<bool> UserExists(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        return user != null;
    }

    public async Task Seed()
    {
        var dtos = new List<Dto>
        {
            new(UserData.UsernameA, UserData.Password),
            new(UserData.UsernameB, UserData.Password)
        };

        foreach (var dto in dtos)
        {
            var username = dto.Username;
            var password = dto.Password;

            var exists = await UserExists(username);
            if (!exists)
            {
                var user = MapToUser(username);
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    var message = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create user {username}: {message}");
                }
            }
        }
    }
}