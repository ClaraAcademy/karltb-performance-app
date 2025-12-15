using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkPositionValue
{
    private static readonly FkFactory _factory = new(nameof(PositionValue));

    public static string PositionId => _factory.Name(nameof(PositionValue.PositionId));
    public static string Bankday => _factory.Name(nameof(PositionValue.Bankday));
}