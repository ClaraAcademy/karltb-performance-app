namespace PerformanceApp.Seeder.Dtos;

public record PortfolioPerformanceDto(
    string PortfolioName,
    string PerformanceType,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    decimal Value
);