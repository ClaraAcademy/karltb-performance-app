using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkPosition
{
    private static readonly FkFactory _factory = new(nameof(Position));

    public static string PortfolioId => _factory.Name(nameof(Position.PortfolioId));
    public static string InstrumentId => _factory.Name(nameof(Position.InstrumentId));
    public static string Bankday => _factory.Name(nameof(Position.Bankday));
}