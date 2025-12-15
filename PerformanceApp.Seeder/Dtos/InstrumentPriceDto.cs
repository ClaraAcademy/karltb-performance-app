namespace PerformanceApp.Seeder.Dtos;
public record InstrumentPriceDto(
    DateOnly Bankday,
    string InstrumentName,
    string InstrumentType,
    decimal Price
);