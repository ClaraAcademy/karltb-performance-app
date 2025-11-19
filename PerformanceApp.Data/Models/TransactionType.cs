namespace PerformanceApp.Data.Models;

public partial class TransactionType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
