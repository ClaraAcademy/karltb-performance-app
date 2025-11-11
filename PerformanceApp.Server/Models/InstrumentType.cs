using System;
using System.Collections.Generic;

namespace PerformanceApp.Server.Models;

public partial class InstrumentType
{
    public int InstrumentTypeId { get; set; }

    public string InstrumentTypeName { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<Instrument> InstrumentsNavigation { get; set; } = [];
}
