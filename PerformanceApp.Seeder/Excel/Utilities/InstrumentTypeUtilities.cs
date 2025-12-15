using PerformanceApp.Seeder.Excel.Constants;

namespace PerformanceApp.Seeder.Excel.Utilities;

using Sv = InstrumentTypeSv;
using En = InstrumentTypeEn;

public class InstrumentTypeUtilities
{
    private static readonly Dictionary<string,string> _mapping = new()
    {
        {
            Sv.Aktie.ToString(), 
            En.Stock.ToString()
        },
        {
            Sv.Obligation.ToString(), 
            En.Bond.ToString()
        },
        {
            Sv.Index.ToString(), 
            En.Index.ToString()
        },
    };

    public static bool IsInstrumentType(string s)
    {
        var sv = Enum.GetNames<Sv>();
        var en = Enum.GetNames<En>();
        
        return sv.Contains(s) || en.Contains(s);
    }

    public static string Normalize(string type)
    {
        return _mapping.GetValueOrDefault(type, type);
    }

    public static bool IsBond(string type)
    {
        var normalizedType = Normalize(type);

        return normalizedType == En.Bond.ToString();
    }
}
