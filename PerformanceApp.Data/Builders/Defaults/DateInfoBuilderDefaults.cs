namespace PerformanceApp.Data.Builders.Defaults;

public static class DateInfoBuilderDefaults
{
    public static DateOnly Bankday => DateOnly.FromDateTime(DateTime.UtcNow);
}