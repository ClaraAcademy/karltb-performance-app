namespace PerformanceApp.Data.Builders.Defaults;

public static class StagingBuilderDefaults
{
    public static DateOnly Bankday => DateOnly.FromDateTime(DateTime.Now);
    public static string InstrumentType => "Default Instrument Type";
    public static string InstrumentName => "Default Staging";
    public static decimal Price => 100.0m;
}