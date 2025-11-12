using System;
using System.Collections.Generic;

namespace PerformanceApp.Data.Models;

public partial class KeyFigureInfo
{
    public int KeyFigureId { get; set; }

    public string KeyFigureName { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<KeyFigureValue> KeyFigureValuesNavigation { get; set; } = [];
}
