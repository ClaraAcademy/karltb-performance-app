SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER FUNCTION padb.ufnGetPerformanceID(@PerformanceTypeName NVARCHAR(MAX))
RETURNS INT
AS
BEGIN
    DECLARE @ID INT;

    SELECT @ID = ID
    FROM padb.PerformanceTypeInfo
    WHERE [Name] = @PerformanceTypeName

    RETURN (@ID);
END
GO