using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string? TransactionTypeName { get; set; }

    public virtual ICollection<Transaction> TransactionsNavigation { get; set; } = [];
}
