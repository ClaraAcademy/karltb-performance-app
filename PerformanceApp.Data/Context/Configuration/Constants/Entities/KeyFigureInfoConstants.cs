using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class KeyFigureInfoConstants : EntityConstants<KeyFigureInfo>
{
    private const string _indexName = "UQ_KeyFigureInfo_Name";

    public static string IndexName => _indexName;
}