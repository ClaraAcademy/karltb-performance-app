namespace PerformanceApp.Server.Auth.Jwt.Duration.Constants;

public static class DurationConstants
{
    private static readonly TimeSpan _defaultDuration = TimeSpan.FromHours(1);
    public static TimeSpan DefaultDuration => _defaultDuration;
}