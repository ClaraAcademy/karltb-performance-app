SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdatePortfolioCumulativeDayPerformance
    @Bankday DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    EXEC padb.CheckBankday
        @Bankday = @Bankday;

    BEGIN TRANSACTION;
        INSERT INTO padb.PortfolioPerformance(PortfolioID, TypeID, PeriodStart, PeriodEnd, [Value])
        SELECT
            PortfolioID,
            padb.ufnGetCumulativeDayPerformanceID(),
            @Bankday,
            @Bankday,
            CASE
                WHEN SUM(CASE WHEN DayPerformance + 1.0 = 0.0 THEN 1 ELSE 0 END) > 0 
                    THEN -1.0
                WHEN SUM(CASE WHEN DayPerformance + 1.0 < 0.0 THEN 1 ELSE 0 END) % 2 = 1
                    THEN -EXP(SUM(LOG(ABS(DayPerformance + 1.0)))) - 1.0
                ELSE EXP(SUM(LOG(DayPerformance + 1.0))) - 1.0
            END
        FROM padb.PortfolioDayPerformance
        WHERE Bankday <= @Bankday
        GROUP BY PortfolioID
    COMMIT;
END
GO