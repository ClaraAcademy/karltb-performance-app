namespace PerformanceApp.Seeder.Excel.Utilities;

public class InstrumentNameUtilities
{
    public static bool IsInstrumentName(string name)
    {
        return !InstrumentTypeUtilities.IsInstrumentType(name);
    }
}