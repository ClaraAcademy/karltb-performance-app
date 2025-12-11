using PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Fks;

public static class FkPositionValue
{
    private static readonly FkFactory _factory = new(nameof(PositionValue));

    public static string PositionId => _factory.Name(nameof(PositionValue.PositionId));
    public static string Bankday => _factory.Name(nameof(PositionValue.Bankday));
}