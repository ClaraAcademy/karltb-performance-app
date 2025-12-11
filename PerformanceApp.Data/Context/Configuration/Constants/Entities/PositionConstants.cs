using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class PositionConstants : EntityConstants<Position>
{
    private const string _amountColumnName = "Amount";
    private const string _nominalColumnName = "Nominal";
    private const string _proportionColumnName = "Proportion";
    private const string _instrumentIdColumnName = "InstrumentID";
    private const string _portfolioIdColumnName = "PortfolioID";
    private const string _amountType = "decimal(19,4)";
    private const string _nominalType = "decimal(19,4)";
    private const string _proportionType = "decimal(5,4)";
    private const string _bankdayForeignKeyName = "FK_Position_Bankday";
    private const string _instrumentIdForeignKeyName = "FK_Position_InstrumentID";
    private const string _portfolioIdForeignKeyName = "FK_Position_PortfolioID";

    public static string AmountColumnName => _amountColumnName;
    public static string NominalColumnName => _nominalColumnName;
    public static string ProportionColumnName => _proportionColumnName;
    public static string InstrumentIdColumnName => _instrumentIdColumnName;
    public static string PortfolioIdColumnName => _portfolioIdColumnName;
    public static string AmountType => _amountType;
    public static string NominalType => _nominalType;
    public static string ProportionType => _proportionType;
    public static string BankdayForeignKeyName => _bankdayForeignKeyName;
    public static string InstrumentIdForeignKeyName => _instrumentIdForeignKeyName;
    public static string PortfolioIdForeignKeyName => _portfolioIdForeignKeyName;

}