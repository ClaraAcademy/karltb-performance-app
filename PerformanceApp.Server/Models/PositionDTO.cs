using System.ComponentModel.DataAnnotations;

namespace PerformanceApp.Server.Models;
public partial class PositionDTO
{
    public int? PortfolioId { get; set; }

    public int? InstrumentId { get; set; }

    public string? InstrumentName { get; set; }

    public DateOnly? Bankday { get; set; }

    [DataType(DataType.Currency)]
    public decimal? Value { get; set; }

    public decimal? UnitPrice { get; set; }
}