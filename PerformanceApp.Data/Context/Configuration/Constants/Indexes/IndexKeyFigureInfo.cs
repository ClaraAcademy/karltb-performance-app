using PerformanceApp.Data.Context.Configuration.Constants.Indexes.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Indexes;

public static class IndexKeyFigureInfo
{
    private static readonly IndexFactory _factory = new(nameof(KeyFigureInfo));

    public static string Name => _factory.Name(nameof(KeyFigureInfo.Name));
}