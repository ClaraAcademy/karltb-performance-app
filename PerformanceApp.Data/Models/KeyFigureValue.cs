using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class KeyFigureValue
{
    public int PortfolioId { get; set; }

    public int KeyFigureId { get; set; }

    public decimal? KeyFigureValue1 { get; set; }

    public DateTime Created { get; set; }

    public virtual KeyFigureInfo KeyFigureInfoNavigation { get; set; } = null!;

    public virtual Portfolio PortfolioNavigation { get; set; } = null!;
}
