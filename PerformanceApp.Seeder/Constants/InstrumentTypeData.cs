namespace PerformanceApp.Seeder.Constants;

public static class InstrumentTypeData
{
    public const string Stock = "Stock";
    public const string Bond = "Bond";
    public const string Index = "Index";

    private static readonly List<string> _instrumentTypes = [Stock, Bond, Index];

    public static List<string> InstrumentTypes => _instrumentTypes.OrderBy(i => i).ToList();
}