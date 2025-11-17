SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdateStandardDeviation
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
        INSERT INTO padb.KeyFigureValue(PortfolioID, KeyFigureID, KeyFigureValue)
        SELECT 
            PortfolioID, 
            padb.ufnGetKeyFigureID('Standard Deviation'), 
            STDEV([Value]) * SQRT(padb.ufnGetAnnualizationFactor())
        FROM padb.PortfolioPerformance
        WHERE TypeID = padb.ufnGetDayPerformanceID()
        GROUP BY PortfolioID;
    COMMIT;
END
GO