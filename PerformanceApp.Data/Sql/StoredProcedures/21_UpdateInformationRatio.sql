SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdateInformationRatio
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
        WITH ActiveReturn AS (
            SELECT 
                pp.PortfolioID AS PortfolioID,
                pp.[Value] - bp.[Value] AS [Value]
            FROM padb.Benchmark AS b
                JOIN padb.PortfolioPerformance AS pp
                    ON b.PortfolioID = pp.PortfolioID
                    AND pp.TypeID = padb.ufnGetDayPerformanceID()
                JOIN padb.PortfolioPerformance AS bp
                    ON b.BenchmarkID = bp.PortfolioID
                    AND bp.TypeID = padb.ufnGetDayPerformanceID()
        )
        INSERT INTO padb.KeyFigureValue(PortfolioID, KeyFigureID, KeyFigureValue)
        SELECT
            PortfolioID,
            padb.ufnGetKeyFigureID('Information Ratio'),
            SQRT(padb.ufnGetAnnualizationFactor()) * AVG([Value]) / STDEV([Value])
        FROM ActiveReturn
        GROUP BY PortfolioID;
    COMMIT;
END
GO