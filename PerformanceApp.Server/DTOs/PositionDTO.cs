using System.ComponentModel.DataAnnotations;

namespace PerformanceApp.Server.Dtos;
public class PositionDTO
{
    public int? PortfolioId { get; set; }

    public int? InstrumentId { get; set; }

    public string? InstrumentName { get; set; }

    public DateOnly? Bankday { get; set; }

    [DataType(DataType.Currency)]
    public decimal? Value { get; set; }

    public decimal? UnitPrice { get; set; }
}
public class BondPositionDTO : PositionDTO
{
    public decimal? Nominal { get; set; }
}

public class IndexPositionDTO : PositionDTO
{
    public decimal? Proportion { get; set; }
}
public class StockPositionDTO : PositionDTO
{
    public int? Count { get; set; }
}