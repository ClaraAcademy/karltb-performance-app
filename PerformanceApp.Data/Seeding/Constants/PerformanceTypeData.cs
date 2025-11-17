namespace PerformanceApp.Data.Seeding.Constants;

public static class PerformanceTypeData
{
    private static readonly string DayPerformance = "Day Performance";
    private static readonly string MonthPerformance = "Month Performance";
    private static readonly string HalfYearPerformance = "Half-Year Performance";
    private static readonly string CumulativeDayPerformance = "Cumulative Day Performance";

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