SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.CheckBankday
    @Bankday DATE
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM padb.DateInfo
        WHERE Bankday = @Bankday
    )
        THROW 50001, 'ERROR: The bank day does not exist', 1;
END
GO