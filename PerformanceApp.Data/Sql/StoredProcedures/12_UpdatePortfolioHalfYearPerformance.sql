SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdatePortfolioHalfYearPerformance
AS
BEGIN
    SET NOCOUNT ON;

    -- Might change this to run with arguments

    BEGIN TRANSACTION;
        INSERT INTO padb.PortfolioPerformance(PortfolioID, TypeID, PeriodStart, PeriodEnd, [Value])
        SELECT
            PortfolioID,
            padb.ufnGetHalfYearPerformanceID(),
            MIN(Bankday),
            MAX(Bankday),
            EXP(SUM(LOG(1.0 + DayPerformance))) - 1.0
        FROM
            padb.PortfolioDayPerformance
        GROUP BY
            PortfolioID;
    COMMIT;
END
GO