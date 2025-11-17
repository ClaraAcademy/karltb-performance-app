namespace PerformanceApp.Data.Seeding.Queries;

public static class KeyFigureQueries
{
    private static readonly FormattableString UpdateStandardDeviation = $@"EXEC padb.uspUpdateStandardDeviation;";
    private static readonly FormattableString UpdateTrackingError = $@"EXEC padb.uspUpdateTrackingError;";
    private static readonly FormattableString UpdateAnnualisedCumulativeReturn = $@"EXEC padb.uspUpdateAnnualisedCumulativeReturn;";
    private static readonly FormattableString UpdateInformationRatio = $@"EXEC padb.uspUpdateInformationRatio;";
    private static readonly FormattableString UpdateHalfYearPerformance = $@"EXEC padb.uspUpdateHalfYearPerformance;";

    public static readonly List<FormattableString> Queries = 
    [
        UpdateStandardDeviation, 
        UpdateTrackingError, 
        UpdateAnnualisedCumulativeReturn, 
        UpdateInformationRatio, 
        UpdateHalfYearPerformance
    ];
}