using PerformanceApp.Data.Context.Configuration.Constants.Indexes.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Indexes;

public static class IndexPerformanceType
{
    private static readonly IndexFactory _factory = new(nameof(PerformanceType));

    public static string Name => _factory.Name(nameof(PerformanceType.Name));
}