using PerformanceApp.Data.Constants;

namespace PerformanceApp.Seeder.Constants;

public static class PerformanceTypeData
{
    private static readonly List<string> _performanceTypes = [
        PerformanceTypeConstants.Day,
        PerformanceTypeConstants.Month,
        PerformanceTypeConstants.HalfYear,
        PerformanceTypeConstants.CumulativeDay
    ];

    public static List<string> PerformanceTypes => _performanceTypes.OrderBy(n => n).ToList();
}