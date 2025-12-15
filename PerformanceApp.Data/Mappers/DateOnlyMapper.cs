using ClosedXML.Excel;

namespace PerformanceApp.Data.Mappers;

public class DateOnlyMapper
{
    public static DateOnly Map(IXLCell cell)
    {
        var dateTime = cell.GetValue<DateTime>();
        return DateOnly.FromDateTime(dateTime);
    }
}