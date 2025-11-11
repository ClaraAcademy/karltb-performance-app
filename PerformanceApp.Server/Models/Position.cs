using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public int? PortfolioId { get; set; }

    public int? InstrumentId { get; set; }

    public DateOnly? Bankday { get; set; }

    public int? Count { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Proportion { get; set; }

    public decimal? Nominal { get; set; }

    public DateTime Created { get; set; }

    public virtual DateInfo? BankdayNavigation { get; set; }

    public virtual Instrument? InstrumentNavigation { get; set; }

    public virtual Portfolio? PortfolioNavigation { get; set; }

    public virtual ICollection<PositionValue> PositionValuesNavigation { get; set; } = [];
}
