using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PortfolioValueConstants : EntityConstants<PortfolioValue>
{
    private const string _bankdayForeignKeyName = "FK_PortfolioValue_Bankday";
    private const string _portfolioIdForeignKeyName = "FK_PortfolioValue_PortfolioID";

    public static string BankdayForeignKeyName => _bankdayForeignKeyName;
    public static string PortfolioIdForeignKeyName => _portfolioIdForeignKeyName;
}