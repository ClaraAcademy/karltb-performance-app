using PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Fks;

public static class FkPortfolio
{
    private static readonly FkFactory _factory = new(nameof(Portfolio));

    public static string UserID => _factory.Name(nameof(Portfolio.UserID));
}