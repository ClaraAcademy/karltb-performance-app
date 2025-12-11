using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class InstrumentConstants : EntityConstants<Instrument>
{
    private const string _indexName = "IX_Instrument_InstrumentName";
    private const string _instrumentTypeForeignKeyName = "FK_Instrument_InstrumentType";

    public static string IndexName => _indexName;
    public static string InstrumentTypeForeignKeyName => _instrumentTypeForeignKeyName;
}