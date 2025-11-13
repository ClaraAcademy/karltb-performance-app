SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspBuyInstrument
    @PortfolioName NVARCHAR(100) = NULL,
    @InstrumentName NVARCHAR(100) = NULL,
    @Count INT = NULL,
    @Amount DECIMAL(19,4) = NULL,
    @Proportion DECIMAL(9,8) = NULL,
    @Nominal DECIMAL(19,4) = NULL,
    @BuyDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PortfolioID INT;
    EXEC padb.uspGetPortfolioID
        @PortfolioName = @PortfolioName,
        @PortfolioID = @PortfolioID OUTPUT;

    DECLARE @InstrumentID INT;
    EXEC padb.uspGetInstrumentID
        @InstrumentName = @InstrumentName,
        @InstrumentID = @InstrumentID OUTPUT;

    EXEC padb.CheckBankday
        @Bankday = @BuyDate;

    EXEC padb.CheckInstrumentWeight
        @InstrumentID = @InstrumentID,
        @Count = @Count,
        @Amount = @Amount,
        @Proportion = @Proportion,
        @Nominal = @Nominal;

    DECLARE @BuyID INT;
    SELECT @BuyID = TransactionTypeID
    FROM padb.TransactionType
    WHERE TransactionTypeName = 'Buy';

    BEGIN TRANSACTION;
        -- --------------------------------------------------------------------------
        -- Add transaction
        -- --------------------------------------------------------------------------
        INSERT INTO padb.[Transaction](
            PortfolioID, 
            InstrumentID, 
            Bankday, 
            TransactionTypeID, 
            [Count], 
            Amount, 
            Proportion, 
            Nominal
        )
        SELECT 
            @PortfolioID, 
            @InstrumentID, 
            @BuyDate, 
            @BuyID, 
            @Count, 
            @Amount, 
            @Proportion, 
            @Nominal;
    COMMIT;
END
GO