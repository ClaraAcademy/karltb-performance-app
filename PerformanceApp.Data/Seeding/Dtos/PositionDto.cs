namespace PerformanceApp.Data.Seeding.Dtos;

public record PositionDto
(
    string PortfolioName,
    string InstrumentName,
    DateOnly Bankday,
    int? Count,
    decimal? Amount,
    decimal? Proportion,
    decimal? Nominal
);