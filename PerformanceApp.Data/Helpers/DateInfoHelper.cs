using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Helpers;

public static class DateInfoHelper
{
    public static List<DateOnly> OrderedBankdays(this IEnumerable<DateInfo> dateInfos)
    {
        return dateInfos
            .OrderBy(di => di.Bankday)
            .Select(di => di.Bankday)
            .ToList();
    }
}