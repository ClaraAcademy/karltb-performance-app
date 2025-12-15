using System.Data;
using ClosedXML.Excel;
using PerformanceApp.Data.Helpers;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Excel.Utilities;

namespace PerformanceApp.Seeder.Excel.Reader;

public static class ExcelReader
{
    private static List<Staging> MapColumn(List<DateOnly> dates, IXLRangeColumn column)
    {
        var populated = IxlRangeColumnHelper.IsPopulated(column);
        if (!populated)
        {
            return [];
        }

        var numRows = column.CellCount();
        var cells = column.Cells(2, numRows); // Skip PRISER on row 1

        var prices = cells
            .Where(IxlCellHelper.IsDecimal)
            .Select(DecimalMapper.Map)
            .ToList();
        var headers = cells
            .Where(IxlCellHelper.IsString)
            .Select(StringMapper.Map)
            .ToList();
        var type = headers
            .Select(InstrumentTypeUtilities.Normalize)
            .Single(InstrumentTypeUtilities.IsInstrumentType);
        var name = headers.Single(InstrumentNameUtilities.IsInstrumentName);

        if (InstrumentTypeUtilities.IsBond(type))
        {
            prices = prices.Select(p => p / 100.0m).ToList();
        }

        return dates
            .Zip(prices, (d,p) => StagingMapper.Map(d, type, name, p))
            .ToList();
    }

    public static List<Staging> ReadExcel(FileInfo file)
    {
        if (!file.Exists)
        {
            return [];
        }
        using var workbook = new XLWorkbook(file.FullName);
        var worksheet = workbook.Worksheet(1);

        var range = worksheet.RangeUsed();

        if (range == null)
        {
            return [];
        }

        var numColumns = range.ColumnCount();

        var dates = IxlRangeHelper.GetDates(range);

        var stagings = range
            .Columns(2, numColumns) // Skip index column
            .SelectMany(c => MapColumn(dates, c))   // Map to Staging and flatten
            .ToList();

        return stagings;
    }

}