SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdateAnnualisedCumulativeReturn
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
        INSERT INTO padb.KeyFigureValue(PortfolioID, KeyFigureID, KeyFigureValue)
        SELECT
            PortfolioID,
            padb.ufnGetKeyFigureID('Annualised Cumulative Return'),
            POWER(
                EXP(SUM(LOG([Value] + 1.0))),
                padb.ufnGetAnnualizationFactor()
            ) - 1.0
        FROM padb.PortfolioPerformance
        WHERE TypeID = padb.ufnGetDayPerformanceID()
        GROUP BY PortfolioID;
    COMMIT;
END
GO