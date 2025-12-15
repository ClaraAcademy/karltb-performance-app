namespace PerformanceApp.Seeder.Dtos;

public record StagingDto
(
    DateOnly Bankday,
    string InstrumentType,
    string InstrumentName,
    decimal Price
);