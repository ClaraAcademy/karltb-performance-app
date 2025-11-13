SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.CheckInstrumentWeight
    @InstrumentID INT = NULL,
    @Count INT = NULL,
    @Amount DECIMAL(19,4) = NULL,
    @Proportion DECIMAL(5,4) = NULL,
    @Nominal DECIMAL(19,4) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InstrumentType NVARCHAR(MAX);

    SELECT @InstrumentType = it.InstrumentTypeName
    FROM padb.Instrument AS i
        JOIN padb.InstrumentType AS it
            ON i.InstrumentTypeID = it.InstrumentTypeID
    WHERE i.InstrumentID = @InstrumentID;

    IF (@InstrumentType = 'Stock' AND @Count IS NULL)
        THROW 5001, 'ERROR: A stock must be bought with @Count set', 1;
    IF (@InstrumentType = 'Bond' AND @Nominal IS NULL)
        THROW 5001, 'ERROR: A stock must be bought with @Nominal set', 1;
    IF (@InstrumentType = 'Index' AND @Proportion IS NULL)
        THROW 5001, 'ERROR: An index must be bought with @Proportion set', 1;
END
GO