namespace PerformanceApp.Seeder.Dtos;

public record PortfolioValueDto
(
    string PortfolioName,
    DateOnly Bankday,
    decimal Value
);