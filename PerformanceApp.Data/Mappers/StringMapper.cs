using ClosedXML.Excel;

namespace PerformanceApp.Data.Mappers;

public class StringMapper
{
    public static string Map(IXLCell cell)
    {
        return cell.Value.ToString();
    }
}