using ClosedXML.Excel;
using PerformanceApp.Data.Mappers;

namespace PerformanceApp.Data.Helpers;

public class IxlRangeHelper
{
    public static List<DateOnly> GetDates(IXLRange range)
    {
        return range
            .Column(1)
            .CellsUsed()
            .Where(IxlCellHelper.IsDate)
            .Select(DateOnlyMapper.Map)
            .ToList();
    }
}