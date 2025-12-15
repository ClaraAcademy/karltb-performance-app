namespace PerformanceApp.Seeder.Constants;

public static class PerformanceTypeData
{
    public static readonly string DayPerformance = "Day Performance";
    public static readonly string MonthPerformance = "Month Performance";
    public static readonly string HalfYearPerformance = "Half-Year Performance";
    public static readonly string CumulativeDayPerformance = "Cumulative Day Performance";

    private static readonly List<string> _performanceTypes = [
        DayPerformance,
        MonthPerformance,
        HalfYearPerformance,
        CumulativeDayPerformance
    ];

    public static List<string> PerformanceTypes => _performanceTypes.OrderBy(n => n).ToList();

    public static List<string> GetPerformanceTypes()
    {
        return [
            DayPerformance,
            MonthPerformance,
            HalfYearPerformance,
            CumulativeDayPerformance
        ];
    }
}