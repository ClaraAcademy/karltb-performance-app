using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class TransactionConstants : EntityConstants<Transaction>
{
    private const string _amountColumnName = "Amount";
    private const string _nominalColumnName = "Nominal";
    private const string _proportionColumnName = "Proportion";
    private const string _instrumentIdColumnName = "InstrumentID";
    private const string _portfolioIdColumnName = "PortfolioID";
    private const string _transactionTypeIdColumnName = "TransactionTypeID";
    private const string _amountType = "decimal(19, 4)";
    private const string _nominalType = "decimal(19, 4)";
    private const string _proportionType = "decimal(5, 4)";
    private const string _bankdayForeignKeyName = "FK_Transaction_Bankday";
    private const string _instrumentForeignKeyName = "FK_Transaction_InstrumentID";
    private const string _portfolioForeignKeyName = "FK_Transaction_PortfolioID";
    private const string _transactionTypeForeignKeyName = "FK_Transaction_TransactionTypeID";


    public const string AmountColumnName = _amountColumnName;
    public const string NominalColumnName = _nominalColumnName;
    public const string ProportionColumnName = _proportionColumnName;
    public const string InstrumentIdColumnName = _instrumentIdColumnName;
    public const string PortfolioIdColumnName = _portfolioIdColumnName;
    public const string TransactionTypeIdColumnName = _transactionTypeIdColumnName;
    public const string AmountType = _amountType;
    public const string NominalType = _nominalType;
    public const string ProportionType = _proportionType;
    public const string BankdayForeignKeyName = _bankdayForeignKeyName;
    public const string InstrumentForeignKeyName = _instrumentForeignKeyName;
    public const string PortfolioForeignKeyName = _portfolioForeignKeyName;
    public const string TransactionTypeForeignKeyName = _transactionTypeForeignKeyName;
}