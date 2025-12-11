using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class KeyFigureValueConstants : EntityConstants<KeyFigureValue>
{
    private const string _portfolioIdColumnName = "PortfolioID";
    private const string _keyFigureIdColumnName = "KeyFigureID";
    private const string _keyFigureIdForeignKeyName = "FK_KeyFigureValue_KeyFigureID";
    private const string _portfolioIdForeignKeyName = "FK_KeyFigureValue_PortfolioID";

    public static string PortfolioIdColumnName => _portfolioIdColumnName;
    public static string KeyFigureIdColumnName => _keyFigureIdColumnName;
    public static string KeyFigureIdForeignKeyName => _keyFigureIdForeignKeyName;
    public static string PortfolioIdForeignKeyName => _portfolioIdForeignKeyName;
}