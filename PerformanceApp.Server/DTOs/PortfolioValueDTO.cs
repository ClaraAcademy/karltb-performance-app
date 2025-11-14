using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Dtos;

public partial class PortfolioValueDTO
{
    public DateOnly Bankday { get; set; }

    public decimal Value { get; set; }
}
