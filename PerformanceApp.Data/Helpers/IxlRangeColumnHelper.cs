using ClosedXML.Excel;

namespace PerformanceApp.Data.Helpers;

public class IxlRangeColumnHelper
{
    public static bool IsPopulated(IXLRangeColumn column)
    {
        return column.CellsUsed().Any();
    }
}