SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspUpdateInstrumentDayPerformance
    @Bankday DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    EXEC padb.CheckBankday
        @Bankday = @Bankday;

    DECLARE @PreviousBankday DATE = padb.ufnGetPreviousBankday(@Bankday);

    BEGIN TRANSACTION;
        INSERT INTO padb.InstrumentPerformance(InstrumentID, TypeID, PeriodStart, PeriodEnd, [Value])
        SELECT
            i.InstrumentID,
            padb.ufnGetDayPerformanceID(),
            @Bankday,
            @Bankday,
            COALESCE(
                curr.Price / prev.Price - 1,
                0
            )
        FROM padb.Instrument AS i
            JOIN padb.InstrumentPrice AS curr
                ON i.InstrumentID = curr.InstrumentID
                    AND curr.Bankday = @Bankday
            JOIN padb.InstrumentPRice AS prev
                ON i.InstrumentID = prev.InstrumentID
                    AND prev.Bankday = @PreviousBankday;
    COMMIT;
END
GO