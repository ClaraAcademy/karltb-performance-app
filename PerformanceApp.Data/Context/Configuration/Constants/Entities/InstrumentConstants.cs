using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class InstrumentConstants : EntityConstants<Instrument>
{
    private const string _indexName = "IX_Instrument_InstrumentName";

    public static string IndexName => _indexName;
}