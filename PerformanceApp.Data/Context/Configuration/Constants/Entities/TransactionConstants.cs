using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class TransactionConstants : EntityConstants<Transaction>
{
    private const string _bankdayForeignKeyName = "FK_Transaction_Bankday";
    private const string _instrumentForeignKeyName = "FK_Transaction_InstrumentID";
    private const string _portfolioForeignKeyName = "FK_Transaction_PortfolioID";
    private const string _transactionTypeForeignKeyName = "FK_Transaction_TransactionTypeID";

    public const string BankdayForeignKeyName = _bankdayForeignKeyName;
    public const string InstrumentForeignKeyName = _instrumentForeignKeyName;
    public const string PortfolioForeignKeyName = _portfolioForeignKeyName;
    public const string TransactionTypeForeignKeyName = _transactionTypeForeignKeyName;
}