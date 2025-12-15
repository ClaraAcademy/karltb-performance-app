using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class ApplicationUserBuilderDefaults
{
    public static string UserId => "default-user-id";
    public static string UserName => "defaultuser";
    public static ApplicationUser User => new ApplicationUserBuilder().Build();

}