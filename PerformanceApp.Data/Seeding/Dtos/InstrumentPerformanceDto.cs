namespace PerformanceApp.Data.Seeding.Dtos;

public record InstrumentPerformanceDto(
    string InstrumentName, 
    string PerformanceType, 
    DateOnly PeriodStart, 
    DateOnly PeriodEnd, 
    decimal Value
);
