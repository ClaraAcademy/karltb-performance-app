namespace PerformanceApp.Data.Seeding.Dtos;
public record InstrumentPriceDto(
    DateOnly Bankday,
    string InstrumentName,
    string InstrumentType,
    decimal Price
);