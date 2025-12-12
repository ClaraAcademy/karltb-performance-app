using PerformanceApp.Data.Context.Configuration.Constants.Indexes.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Indexes;

public static class IndexPortfolio
{
    private static readonly IndexFactory _factory = new(nameof(Portfolio));

    public static string Name => _factory.Name(nameof(Portfolio.Name));
}