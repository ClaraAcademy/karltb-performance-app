using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class PositionValue
{
    public int PositionId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal? Value { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo BankdayNavigation { get; set; } = null!;

    public virtual Position PositionNavigation { get; set; } = null!;
}
