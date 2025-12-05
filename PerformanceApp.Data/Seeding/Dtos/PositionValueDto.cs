namespace PerformanceApp.Data.Seeding.Dtos;

public record PositionValueDto(
    string PortfolioName, 
    string InstrumentName, 
    DateOnly Bankday, 
    decimal Value
);