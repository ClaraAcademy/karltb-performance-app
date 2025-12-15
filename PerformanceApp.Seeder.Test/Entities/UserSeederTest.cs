using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class UserSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
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

        // Assert
        await AssertUserExists(_userManager, usernameA);
        await AssertUserExists(_userManager, usernameB);
    }

}