using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PositionConstants : EntityConstants<Position>
{
    private const string _portfolioIdColumnName = "PortfolioID";
    private const string _amountType = "decimal(19,4)";
    private const string _nominalType = "decimal(19,4)";
    private const string _proportionType = "decimal(5,4)";
    private const string _bankdayForeignKeyName = "FK_Position_Bankday";
    private const string _instrumentIdForeignKeyName = "FK_Position_InstrumentID";
    private const string _portfolioIdForeignKeyName = "FK_Position_PortfolioID";

    public static string PortfolioIdColumnName => _portfolioIdColumnName;
    public static string AmountType => _amountType;
    public static string NominalType => _nominalType;
    public static string ProportionType => _proportionType;
    public static string BankdayForeignKeyName => _bankdayForeignKeyName;
    public static string InstrumentIdForeignKeyName => _instrumentIdForeignKeyName;
    public static string PortfolioIdForeignKeyName => _portfolioIdForeignKeyName;

}