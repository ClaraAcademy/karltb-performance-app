namespace PerformanceApp.Data.Seeding.Dtos;

public record PortfolioValueDto
(
    string PortfolioName,
    DateOnly Bankday,
    decimal Value
);