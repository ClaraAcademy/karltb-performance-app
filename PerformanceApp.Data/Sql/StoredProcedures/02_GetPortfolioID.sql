SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspGetPortfolioID
    @PortfolioName NVARCHAR(100) = NULL,
    @PortfolioID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @PortfolioID = NULL;

    SELECT @PortfolioID = PortfolioID
    FROM padb.Portfolio
    WHERE PortfolioName = @PortfolioName;
    
    IF (@PortfolioID IS NULL)
        THROW 50001, 'ERROR: The portfolio does not exist', 1;
END
GO