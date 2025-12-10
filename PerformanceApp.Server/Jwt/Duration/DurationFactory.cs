using PerformanceApp.Server.Jwt.Duration.Constants;

namespace PerformanceApp.Server.Jwt.Duration;

public static class DurationFactory
{
    public static DateTime CreateDefaultExpiryTime()
    {
        var duration = DurationConstants.DefaultDuration;

        return DateTime.UtcNow.AddHours(duration.TotalHours);
    }
}