SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdatePortfolioValue
    @Bankday DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    EXEC padb.CheckBankday
        @Bankday = @Bankday;
    
    BEGIN TRANSACTION;
        INSERT INTO padb.PortfolioValue(PortfolioID, Bankday, PortfolioValue)
        SELECT
            por.PortfolioID,
            @Bankday,
            SUM(pv.PositionValue)
        FROM padb.Portfolio AS por
            JOIN padb.Position AS pos
                ON por.PortfolioID = pos.PortfolioID
                    AND pos.Bankday = @Bankday
            JOIN padb.PositionValue AS pv
                ON pos.PositionID = pv.PositionID
        GROUP BY por.PortfolioID;
    COMMIT;
END
GO