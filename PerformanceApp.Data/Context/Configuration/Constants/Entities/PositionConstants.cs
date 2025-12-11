using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PositionConstants : EntityConstants<Position>
{
    private const string _bankdayForeignKeyName = "FK_Position_Bankday";
    private const string _instrumentIdForeignKeyName = "FK_Position_InstrumentID";
    private const string _portfolioIdForeignKeyName = "FK_Position_PortfolioID";

    public static string BankdayForeignKeyName => _bankdayForeignKeyName;
    public static string InstrumentIdForeignKeyName => _instrumentIdForeignKeyName;
    public static string PortfolioIdForeignKeyName => _portfolioIdForeignKeyName;

}