using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkKeyFigureValue
{
    private static readonly FkFactory _factory = new(nameof(KeyFigureValue));

    public static string KeyFigureId => _factory.Name(nameof(KeyFigureValue.KeyFigureId));
    public static string PortfolioId => _factory.Name(nameof(KeyFigureValue.PortfolioId));
}