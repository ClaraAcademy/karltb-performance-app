using ClosedXML.Excel;

namespace PerformanceApp.Data.Mappers;

public class DecimalMapper
{
    public static decimal Map(IXLCell cell)
    {
        var value = cell.GetValue<string>();
        return decimal.Parse(value);
    }

}