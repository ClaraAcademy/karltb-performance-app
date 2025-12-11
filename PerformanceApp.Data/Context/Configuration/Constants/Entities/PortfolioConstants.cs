using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PortfolioConstants : EntityConstants<Portfolio>
{
    private const string _indexName = "UQ_Portfolio_Name";

    public static string IndexName => _indexName;
}