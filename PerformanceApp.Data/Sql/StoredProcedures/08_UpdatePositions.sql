SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdatePositions
    @Bankday DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    EXEC padb.CheckBankday
        @Bankday = @Bankday;

    -- --------------------------------------------------------------------------
    -- Get previous bankday
    -- --------------------------------------------------------------------------
    DECLARE @PreviousBankday DATE = padb.ufnGetPreviousBankday(@Bankday);

    BEGIN TRANSACTION;
        -- ----------------------------------------------------------------------
        -- Insert new positions
        -- ----------------------------------------------------------------------
        INSERT INTO padb.[Position](
            PortfolioID, InstrumentID, Bankday, [Count], Amount, Proportion, Nominal
        )
        SELECT 
            PortfolioID, 
            InstrumentID, 
            @Bankday, 
            SUM(COALESCE([Count],    0)), 
            SUM(COALESCE(Amount,     0)), 
            SUM(COALESCE(Proportion, 0)), 
            SUM(COALESCE(Nominal,    0))
        FROM (
            -- Yesterday's positions
            SELECT 
                PortfolioID, 
                InstrumentID, 
                [Count], 
                Amount, 
                Proportion, 
                Nominal
            FROM padb.[Position]
            WHERE Bankday = @PreviousBankday

            UNION ALL

            -- Today's transactions
            SELECT 
                PortfolioID, 
                InstrumentID, 
                [Count], 
                Amount, 
                Proportion, 
                Nominal
            FROM padb.[Transaction]
            WHERE Bankday = @Bankday
        ) AS combined
        GROUP BY PortfolioID, InstrumentID;

        -- ----------------------------------------------------------------------
        -- Delete empty positions
        -- ----------------------------------------------------------------------
        DELETE FROM padb.Position
        WHERE Bankday = @Bankday
            AND ([Count] + [Amount] + [Proportion] + [Nominal]) = 0;
        -- ----------------------------------------------------------------------
        -- Update position values
        -- ----------------------------------------------------------------------
        INSERT INTO padb.PositionValue(PositionID, Bankday, PositionValue)
        SELECT
            pos.PositionID,
            @Bankday,
            (
                pos.[Count] + pos.[Amount] + pos.[Proportion] + pos.[Nominal]
            ) * ip.price
        FROM padb.Position AS pos
            JOIN padb.InstrumentPrice AS ip
                ON pos.Bankday = @Bankday
                AND ip.Bankday = @Bankday
                AND pos.Bankday = ip.Bankday
                AND pos.InstrumentID = ip.InstrumentID
        WHERE pos.PortfolioID IN (SELECT PortfolioID FROM padb.Benchmark); -- Only use actual portfolios

    COMMIT;
END
GO