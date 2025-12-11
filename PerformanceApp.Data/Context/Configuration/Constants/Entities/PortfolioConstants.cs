using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PortfolioConstants : EntityConstants<Portfolio>
{
    private const string _indexName = "UQ_Portfolio_Name";
    private const string _userIdForeignKeyName = "FK_Portfolio_UserID";

    public static string IndexName => _indexName;
    public static string UserIdForeignKeyName => _userIdForeignKeyName;
}