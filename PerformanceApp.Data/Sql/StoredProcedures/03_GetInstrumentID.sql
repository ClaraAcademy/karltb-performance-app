SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspGetInstrumentID
    @InstrumentName NVARCHAR(100) = NULL,
    @InstrumentID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @InstrumentID = NULL;
    
    SELECT @InstrumentID = InstrumentID
    FROM padb.Instrument
    WHERE InstrumentName = @InstrumentName;

    IF (@InstrumentID IS NULL)
        THROW 50001, 'ERROR: The instrument does not exist', 1;
END
GO