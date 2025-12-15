using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services.Helpers;

namespace PerformanceApp.Data.Mappers;

public static class PositionMapper
{
    public static StockPositionDTO MapToStockPositionDTO(Position p)
    {
        var dto = new StockPositionDTO { Count = p.Count };
        return MapToDTO(p, dto);
    }

    public static BondPositionDTO MapToBondPositionDTO(Position p)
    {
        var dto = new BondPositionDTO { Nominal = p.Nominal };
        return MapToDTO(p, dto);
    }

    public static IndexPositionDTO MapToIndexPositionDTO(Position p)
    {
        var dto = new IndexPositionDTO { Proportion = p.Proportion };
        return MapToDTO(p, dto);
    }

    private static TBase MapToDTO<TBase>(Position p, TBase dto)
        where TBase : PositionDTO
    {
        dto.PortfolioId = p.PortfolioId;
        dto.InstrumentId = p.InstrumentId;
        dto.InstrumentName = PositionHelper.GetInstrumentName(p);
        dto.Bankday = p.Bankday;
        dto.Value = PositionHelper.GetPositionValue(p);
        dto.UnitPrice = PositionHelper.GetInstrumentUnitPrice(p);
        return dto;
    }
}
