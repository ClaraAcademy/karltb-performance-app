using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class TransactionTypeConstants : EntityConstants<TransactionType>
{
    private const string _indexName = "UQ_TransactionType_TransactionTypeName";

    public const string IndexName = _indexName;
}