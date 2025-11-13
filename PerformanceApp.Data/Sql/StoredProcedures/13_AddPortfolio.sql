SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspAddPortfolio
    @Username NVARCHAR(20),
    @PortfolioName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserID INT;
    SELECT @UserID = UserID
    FROM padb.[USER]
    WHERE Username = @Username;

    BEGIN TRANSACTION;
        INSERT INTO padb.Portfolio
            (UserID, PortfolioName)
        VALUES 
            (@UserID, @PortfolioName);
    COMMIT;
END
GO