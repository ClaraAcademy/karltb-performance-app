using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class PositionValue
{
    public int PositionId { get; set; }

    public DateOnly Bankday { get; set; }

    public decimal? PositionValue1 { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo BankdayNavigation { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
