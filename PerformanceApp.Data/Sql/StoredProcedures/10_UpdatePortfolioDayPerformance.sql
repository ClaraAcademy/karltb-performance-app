SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdatePortfolioDayPerformance
    @Bankday DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    EXEC padb.CheckBankday
        @Bankday = @Bankday;

    DECLARE @PreviousBankday DATE = padb.ufnGetPreviousBankday(@Bankday);

    BEGIN TRANSACTION;
        INSERT INTO padb.PortfolioPerformance(PortfolioID, TypeID, PeriodStart, PeriodEnd, [Value])
        ----------------------------------------------------------------------------------------------------------------
        -- Actual Portfolios
        ----------------------------------------------------------------------------------------------------------------
        SELECT
            p.PortfolioID,
            padb.ufnGetDayPerformanceID(),
            @Bankday,
            @Bankday,
            COALESCE(
                curr.PortfolioValue / prev.PortfolioValue - 1,
                0
            )
        FROM padb.Portfolio AS p
            JOIN padb.PortfolioValue AS curr
                ON p.PortfolioID = curr.PortfolioID
                    AND curr.Bankday = @Bankday
            JOIN padb.PortfolioValue AS prev
                ON p.PortfolioID = prev.PortfolioID
                    AND prev.Bankday = @PreviousBankday
        WHERE p.PortfolioID IN (SELECT PortfolioID FROM padb.Benchmark)

        UNION ALL

        ----------------------------------------------------------------------------------------------------------------
        -- Benchmark Portfolios
        ----------------------------------------------------------------------------------------------------------------
        SELECT
            pos.PortfolioID,
            padb.ufnGetDayPerformanceID(),
            @Bankday,
            @Bankday,
            SUM(pos.Proportion * ip.DayPerformance)
        FROM padb.Position AS pos
            JOIN padb.InstrumentDayPerformance AS ip
                ON pos.InstrumentID = ip.InstrumentID
                    AND pos.Bankday = @Bankday
                    AND ip.Bankday = @Bankday
        WHERE pos.PortfolioID IN (SELECT BenchmarkID FROM padb.Benchmark)
        GROUP BY pos.PortfolioID;


    COMMIT;
END
GO