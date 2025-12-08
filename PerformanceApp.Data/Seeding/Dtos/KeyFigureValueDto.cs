namespace PerformanceApp.Data.Seeding.Dtos;

public record KeyFigureValueDto(
    string PortfolioName,
    string KeyFigureName,
    decimal Value
);
