namespace PerformanceApp.Data.Seeding.Constants;

public static class PerformanceTypeData
{
    public static readonly string DayPerformance = "Day Performance";
    public static readonly string MonthPerformance = "Month Performance";
    public static readonly string HalfYearPerformance = "Half-Year Performance";
    public static readonly string CumulativeDayPerformance = "Cumulative Day Performance";

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