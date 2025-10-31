using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? PortfolioId { get; set; }

    public int? InstrumentId { get; set; }

    public DateOnly? Bankday { get; set; }

    public int? TransactionTypeId { get; set; }

    public int? Count { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Proportion { get; set; }

    public decimal? Nominal { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo? BankdayNavigation { get; set; }

    public virtual Instrument? Instrument { get; set; }

    public virtual Portfolio? Portfolio { get; set; }

    public virtual TransactionType? TransactionType { get; set; }
}
