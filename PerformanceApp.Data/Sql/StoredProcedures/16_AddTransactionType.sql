SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspAddTransactionType
    @TransactionTypeName NVARCHAR(100) = NULL
AS
BEGIN
    BEGIN TRANSACTION;
        INSERT INTO padb.TransactionType(TransactionTypeName)
        VALUES (@TransactionTypeName);
    COMMIT;
END
GO