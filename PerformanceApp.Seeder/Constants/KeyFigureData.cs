namespace PerformanceApp.Seeder.Constants;

public static class KeyFigureData
{
    public static readonly string StandardDeviation = "Standard Deviation";
    public static readonly string TrackingError = "Tracking Error";
    public static readonly string AnnualisedCumulativeReturn = "Annualised Cumulative Return";
    public static readonly string InformationRatio = "Information Ratio";
    public static readonly string HalfYearPerformance = "Half-Year Performance";

    public static List<string> GetKeyFigures()
    {
        return [
            StandardDeviation,
            TrackingError,
            AnnualisedCumulativeReturn,
            InformationRatio,
            HalfYearPerformance
        ];
    }
}