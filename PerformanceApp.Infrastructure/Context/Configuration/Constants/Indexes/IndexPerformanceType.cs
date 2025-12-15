using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes;

public static class IndexPerformanceType
{
    private static readonly IndexFactory _factory = new(nameof(PerformanceType));

    public static string Name => _factory.Name(nameof(PerformanceType.Name));
}