using PerformanceApp.Server.Auth.Jwt.Duration.Constants;

namespace PerformanceApp.Server.Auth.Jwt.Duration;

public static class DurationFactory
{
    public static DateTime CreateDefaultExpiryTime()
    {
        var duration = DurationConstants.DefaultDuration;

        return DateTime.UtcNow.AddHours(duration.TotalHours);
    }
}