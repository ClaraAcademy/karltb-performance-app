using System.Data;
using ClosedXML.Excel;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Seeder;

public static class ExcelReader
{
    private enum InstrumentTypeSV { Aktie, Obligation, Index }
    private enum InstrumentTypeEN { Stock, Bond, Index }
    private static bool IsPopulated(IXLRangeColumn column) => column.CellsUsed().Any();
    private static bool IsInstrumentType(string s)
    {
        return Enum.IsDefined(typeof(InstrumentTypeSV), s) || Enum.IsDefined(typeof(InstrumentTypeEN), s);
    }
    private static bool IsInstrumentName(string s) => !IsInstrumentType(s);
    private static bool IsDate(IXLCell cell) => cell.TryGetValue<DateTime>(out _);
    private static bool IsDecimal(IXLCell cell) => decimal.TryParse(cell.GetString(), out _);
    private static bool IsString(IXLCell cell)
    {
        return !IsDate(cell)
            && !IsDecimal(cell)
            && !string.IsNullOrWhiteSpace(cell.GetString());
    }

    private static DateOnly MapToDate(IXLCell cell)
    {
        var dateTime = cell.GetValue<DateTime>();
        return DateOnly.FromDateTime(dateTime);
    }
    private static decimal MapToDecimal(IXLCell cell) => decimal.Parse(cell.Value.ToString());
    private static string MapToString(IXLCell cell) => cell.Value.ToString();
    private static string NormalizeInstrumentType(string type)
    {
        var mapping = new Dictionary<string, string>
        {
            {
                InstrumentTypeSV.Aktie.ToString(),
                InstrumentTypeEN.Stock.ToString()
            },
            {
                InstrumentTypeSV.Obligation.ToString(),
                InstrumentTypeEN.Bond.ToString()
            },
            {
                InstrumentTypeSV.Index.ToString(),
                InstrumentTypeEN.Index.ToString()
            },
        };
        return mapping.GetValueOrDefault(type, type);
    }
    private static Staging MapToStaging(DateOnly bankday, string type, string name, decimal price)
    {
        return new Staging
        {
            Bankday = bankday,
            InstrumentType = NormalizeInstrumentType(type),
            InstrumentName = name,
            Price = price
        };
    }
    private static List<DateOnly> GetDates(IXLRange range)
    {
        return range.Column(1)
            .CellsUsed()
            .Where(IsDate)
            .Select(MapToDate)
            .ToList();
    }

    private static bool IsBond(string type)
    {
        var normalizedType = NormalizeInstrumentType(type);

        return normalizedType == InstrumentTypeEN.Bond.ToString();
    }

    private static List<Staging> MapColumn(List<DateOnly> dates, IXLRangeColumn column)
    {
        if (!IsPopulated(column))
        {
            return [];
        }
        var numRows = column.CellCount();
        var cells = column.Cells(2, numRows); // Skip PRISER on row 1

        var prices = cells.Where(IsDecimal).Select(MapToDecimal).ToList();

        var headers = cells.Where(IsString).Select(MapToString).ToList();
        var type = headers.Single(IsInstrumentType);
        var name = headers.Single(IsInstrumentName);

        if (IsBond(type))
        {
            prices = prices.Select(p => p / 100.0m).ToList();
        }

        Staging mapToStaging(DateOnly date, decimal price) => MapToStaging(date, type, name, price);

        return dates.Zip(prices, mapToStaging).ToList();
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

        var dates = GetDates(range);

        var stagings = range.Columns(2, numColumns) // Skip index column
            .SelectMany(c => MapColumn(dates, c))   // Map to Staging and flatten
            .ToList();

        return stagings;
    }

}