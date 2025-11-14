SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER FUNCTION padb.ufnGetHalfYearPerformanceID()
RETURNS INT
AS
BEGIN
    DECLARE @ID INT;
    SELECT @ID = padb.ufnGetPerformanceID('Half-Year Performance');
    RETURN (@ID);
END
GO