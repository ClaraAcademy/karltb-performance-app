using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class KeyFigureValue
{
    public int PortfolioId { get; set; }

    public int KeyFigureId { get; set; }

    public decimal? KeyFigureValue1 { get; set; }

    public DateTime Created { get; set; }

    public virtual KeyFigureInfo KeyFigure { get; set; } = null!;

    public virtual Portfolio Portfolio { get; set; } = null!;
}
