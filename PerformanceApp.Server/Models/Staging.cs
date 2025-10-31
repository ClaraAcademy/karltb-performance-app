using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class Staging
{
    public DateOnly? Bankday { get; set; }

    public string? InstrumentType { get; set; }

    public string? InstrumentName { get; set; }

    public decimal? Price { get; set; }

    public DateTime Created { get; set; }
}
