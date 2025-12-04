using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

public class UserSeederTest : BaseSeederTest
{
    private readonly UserSeeder _seeder;
    public UserSeederTest() : base()
    {
        _seeder = new UserSeeder(_userManager);
    }

    private static async Task AssertUserExists(UserManager<ApplicationUser> userManager, string username)
    {
        var user = await userManager.FindByNameAsync(username);
        Assert.NotNull(user);
        Assert.Equal(user.UserName, username);
    }

    [Fact]
    public async Task Seed_AddsUsers_WhenDatabaseIsEmpty()
    {
        // Arrange
        var usernameA = UserData.UsernameA;
        var usernameB = UserData.UsernameB;

        // Act
        await _seeder.Seed();

        // Assert
        await AssertUserExists(_userManager, usernameA);
        await AssertUserExists(_userManager, usernameB);
    }

}