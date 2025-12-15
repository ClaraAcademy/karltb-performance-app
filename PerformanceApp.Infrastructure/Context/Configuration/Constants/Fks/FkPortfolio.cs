using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkPortfolio
{
    private static readonly FkFactory _factory = new(nameof(Portfolio));

    public static string UserID => _factory.Name(nameof(Portfolio.UserID));
}