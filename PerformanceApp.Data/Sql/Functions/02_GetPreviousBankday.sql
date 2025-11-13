SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER FUNCTION padb.ufnGetPreviousBankday(@Bankday DATE)
RETURNS DATE
AS
BEGIN
    DECLARE @PreviousBankday DATE;

    SELECT TOP 1 @PreviousBankday = Bankday
    FROM DateInfo
    WHERE Bankday < @Bankday
    ORDER BY Bankday DESC;

    -- Impossible date
    IF (@PreviousBankday IS NULL)
    BEGIN
        SELECT @PreviousBankday = MIN(Bankday)
        FROM padb.DateInfo;
    END

    RETURN (@PreviousBankday);
END
GO