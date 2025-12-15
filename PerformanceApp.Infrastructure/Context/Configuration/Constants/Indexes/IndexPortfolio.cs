using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes;

public static class IndexPortfolio
{
    private static readonly IndexFactory _factory = new(nameof(Portfolio));

    public static string Name => _factory.Name(nameof(Portfolio.Name));
}