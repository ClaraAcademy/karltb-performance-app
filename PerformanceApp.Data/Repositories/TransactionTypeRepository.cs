using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface ITransactionTypeRepository
{
    EntityEntry<TransactionType>? AddTransactionType(TransactionType transactionType);
    List<EntityEntry<TransactionType>?> AddTransactionTypes(List<TransactionType> transactionType);

}

public class TransactionTypeRepository(PadbContext context) : ITransactionTypeRepository
{
    private readonly PadbContext _context = context;

    private bool Equal(TransactionType lhs, TransactionType rhs)
    {
        return lhs.TransactionTypeName == rhs.TransactionTypeName;
    }
    private bool Exists(TransactionType transactionType)
    {
        return _context.TransactionTypes.Any(tT => Equal(tT, transactionType));
    }
    public EntityEntry<TransactionType>? AddTransactionType(TransactionType transactionType)
    {
        if (Exists(transactionType))
        {
            return null;
        }
        return _context.TransactionTypes.Add(transactionType);
    }
    public List<EntityEntry<TransactionType>?> AddTransactionTypes(List<TransactionType> transactionType)
    {
        return transactionType.Select(AddTransactionType).ToList();
    }
}