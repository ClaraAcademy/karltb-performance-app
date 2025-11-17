SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdateHalfYearPerformance
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
        INSERT INTO padb.KeyFigureValue(PortfolioID, KeyFigureID, KeyFigureValue)
        SELECT
            PortfolioID,
            padb.ufnGetKeyFigureID('Half-Year Performance'),
            [Value]
        FROM padb.PortfolioPerformance
        WHERE TypeID = padb.ufnGetHalfYearPerformanceID();
    COMMIT;
END
GO