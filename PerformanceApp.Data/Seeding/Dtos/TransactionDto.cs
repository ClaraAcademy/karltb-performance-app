namespace PerformanceApp.Data.Seeding.Dtos;

public record TransactionDto(
    string PortfolioName,
    string InstrumentName,
    DateOnly Bankday,
    int? Count = null,
    decimal? Amount = null,
    decimal? Nominal = null,
    decimal? Proportion = null
);