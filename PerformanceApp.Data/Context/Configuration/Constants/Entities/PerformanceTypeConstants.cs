using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PerformanceTypeConstants : EntityConstants<PerformanceType>
{
    private const string _indexName = "UQ_PerformanceTypeInfo_Name";

    public static string IndexName => _indexName;
}