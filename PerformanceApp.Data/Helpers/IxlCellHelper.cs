using ClosedXML.Excel;

namespace PerformanceApp.Data.Helpers;

public class IxlCellHelper
{
    public static bool IsDate(IXLCell cell)
    {
        return cell.TryGetValue<DateTime>(out _);
    }

    public static bool IsDecimal(IXLCell cell)
    {
        return decimal.TryParse(cell.GetString(), out _);
    }

    public static bool IsString(IXLCell cell)
    {
        var notDate = !IsDate(cell);
        var notDecimal = !IsDecimal(cell);
        var notWhitespace = !string.IsNullOrWhiteSpace(cell.GetString());

        return notDate && notDecimal && notWhitespace;
    }
}