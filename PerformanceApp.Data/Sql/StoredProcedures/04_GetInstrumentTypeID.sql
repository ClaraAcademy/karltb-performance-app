SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspGetInstrumentTypeID
    @InstrumentTypeName NVARCHAR(100) = NULL,
    @InstrumentTypeID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @InstrumentTypeID = NULL;

    SELECT @InstrumentTypeID = InstrumentTypeID
    FROM padb.InstrumentType
    WHERE InstrumentTypeName = @InstrumentTypeName;

    IF (@InstrumentTypeID IS NULL)
        THROW 50001, 'ERROR: The InstrumentTypeID does not exist', 1;

END
GO