SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspImportExcelData
AS
BEGIN
    SET NOCOUNT ON;
    -- -----------------------------------------------------------------------------------------------------------------
    -- Read CSV data into string table
    -- -----------------------------------------------------------------------------------------------------------------
    DROP TABLE IF EXISTS #Dirty;
    CREATE TABLE #Dirty
    (
    	Bankday         NVARCHAR(MAX),
    	InstrumentType  NVARCHAR(MAX),
    	InstrumentName  NVARCHAR(MAX),
    	InstrumentPrice NVARCHAR(MAX),
    );
    BULK INSERT #Dirty
    FROM 'C:\Data\prices.csv'
    WITH (
    	ROWTERMINATOR ='\n',
    	FIELDTERMINATOR = ','
    );
    -- -----------------------------------------------------------------------------------------------------------------
    -- Convert string table to corresponding datatypes
    -- -----------------------------------------------------------------------------------------------------------------
    DROP TABLE IF EXISTS #Clean;
    CREATE TABLE #Clean
    (
    	Bankday         DATE,
    	InstrumentType  NVARCHAR(100),
    	InstrumentName  NVARCHAR(100),
    	InstrumentPrice DECIMAL(19,4)
    );
    INSERT INTO #Clean
    SELECT
    	CAST(Bankday 		 AS DATE),
    	CAST(InstrumentType  AS NVARCHAR(100)),
    	CAST(InstrumentName  AS NVARCHAR(100)),
    	CAST(InstrumentPrice AS DECIMAL(19,4))
    FROM #Dirty;

    BEGIN TRANSACTION;
        -- -------------------------------------------------------------------------------------------------------------
        -- Insert potentially new, clean, data into Staging.
        -- -------------------------------------------------------------------------------------------------------------
        INSERT INTO padb.Staging(Bankday, InstrumentType, InstrumentName, Price)
        SELECT
            Bankday,
            InstrumentType,
            InstrumentName,
            InstrumentPrice
        FROM #Clean

        EXCEPT 
    
        SELECT
            Bankday,
            InstrumentType,
            InstrumentName,
            Price
        FROM padb.Staging;

        -- -------------------------------------------------------------------------------------------------------------
        -- Insert potentially new Bankdays
        -- -------------------------------------------------------------------------------------------------------------
        INSERT INTO padb.DateInfo(Bankday)
        SELECT DISTINCT Bankday
        FROM (
            SELECT Bankday
            FROM padb.Staging

            EXCEPT

            SELECT Bankday
            FROM padb.DateInfo
        ) AS tmp
        ORDER BY Bankday;
        -- -------------------------------------------------------------------------------------------------------------
        -- Insert potentially new Instrument Types
        -- -------------------------------------------------------------------------------------------------------------
        INSERT INTO padb.InstrumentType(InstrumentTypeName)
        SELECT DISTINCT InstrumentType
        FROM padb.Staging

        EXCEPT

        SELECT DISTINCT InstrumentTypeName
        FROM padb.InstrumentType;
        -- -------------------------------------------------------------------------------------------------------------
        -- Insert potentially new Instruments
        -- -------------------------------------------------------------------------------------------------------------
        INSERT INTO padb.Instrument(InstrumentTypeID, InstrumentName)
        SELECT DISTINCT
            it.InstrumentTypeID,
            s.InstrumentName
        FROM padb.Staging AS s
            JOIN padb.InstrumentType AS it
                ON s.InstrumentType = it.InstrumentTypeName
        
        EXCEPT

        SELECT DISTINCT
            InstrumentTypeID,
            InstrumentName
        FROM padb.Instrument;
        -- -------------------------------------------------------------------------------------------------------------
        -- Insert potentially new Instrument Prices
        -- -------------------------------------------------------------------------------------------------------------
        INSERT INTO padb.InstrumentPrice(InstrumentID, Bankday, Price)
        SELECT DISTINCT
            i.InstrumentID,
            s.Bankday,
            s.Price
        FROM padb.Staging AS S
            JOIN padb.Instrument AS i
                ON s.InstrumentName = i.InstrumentName

        EXCEPT

        SELECT DISTINCT
            InstrumentID,
            Bankday,
            Price
        FROM padb.InstrumentPrice;
    COMMIT;
    -- -----------------------------------------------------------------------------------------------------------------
    -- Cleanup temporary tables
    -- -----------------------------------------------------------------------------------------------------------------
    DROP TABLE IF EXISTS #Dirty;
    DROP TABLE IF EXISTS #Clean;
END
GO